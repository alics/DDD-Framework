using Framework.Logging.Elasticsearch.Data;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Logging.Elasticsearch.ESClient
{
    public  class LoggingElasticClient<TLogObject> where TLogObject : BaseLogObject
    {
        private readonly ElasticClient _elasticClient;
        public LoggingElasticClient(IConfiguration configuration)
        {
            ConnectionSettings connSettings = new ConnectionSettings(new Uri(configuration.GetConnectionString("ActivityLogConnectionString")));
            connSettings.DefaultIndex("default");
            _elasticClient = new ElasticClient(connSettings);
        }
        public  ElasticClient GetElasticClient(string conectionString)
        {
            return _elasticClient;
        }
        public  IPromise<IIndexSettings> CommonIndexDescriptor(IndexSettingsDescriptor descriptor)
        {
            return descriptor
                .NumberOfReplicas(0)
                .NumberOfShards(1)
                .Analysis(InitCommonAnalyzers);
        }

        public  IAnalysis InitCommonAnalyzers(AnalysisDescriptor analysis)
        {
            return analysis.Analyzers(a => a
                .Custom("html_stripper", cc => cc
                    .Filters("eng_stopwords", "trim", "lowercase")
                    .CharFilters("html_strip")
                    .Tokenizer("autocomplete")
                )
                .Custom("keywords_wo_stopwords", cc => cc
                    .Filters("eng_stopwords", "trim", "lowercase")
                    .CharFilters("html_strip")
                    .Tokenizer("key_tokenizer")
                )
                .Custom("autocomplete", cc => cc
                    .Filters("eng_stopwords", "trim", "lowercase")
                    .Tokenizer("autocomplete")
                )
            )
            .Tokenizers(tdesc => tdesc
                .Keyword("key_tokenizer", t => t)
                .EdgeNGram("autocomplete", e => e
                    .MinGram(3)
                    .MaxGram(15)
                    .TokenChars(TokenChar.Letter, TokenChar.Digit)
                )
            )
            .TokenFilters(f => f
                .Stop("eng_stopwords", lang => lang
                    .StopWords("_english_")
                )
            );
        }

        public  CreateIndexResponse CreateIndex(string indexName)
        {
           
            return _elasticClient.Indices.Create(indexName, index => index
                       .Settings(CommonIndexDescriptor)
                       .Map<TLogObject>(
                        x => x.AutoMap()
                       )
                   );
        }
        public IndexResponse Index(TLogObject logObject, string indexName)
        {
            return _elasticClient.Index(logObject, i => i.Index(indexName));
        }
    }
}
