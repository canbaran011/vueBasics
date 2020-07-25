using Iris.ChatBot.Constants;
using Iris.ChatBot.DAL.Helpers;
using Iris.ChatBot.DAL.Metadata; 
using Iris.ChatBot.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.ChatBot.BL.Codes.Helpers
{
    public class InitializationManager
    {
     
      public static void InitializeGrammarManager()
      {


          int randomCustomerId = 0;
          if (!GrammarManager.IsInitialized)
          {
              List<CustomerModel> customers = new CustomerProvider().SelectAllItems();

              Dictionary<int, java.util.HashMap> reservedWordDataHashMapByCustomer = new Dictionary<int, java.util.HashMap>();
              Dictionary<int, string[]> frequentWordArrayByCustomer = new Dictionary<int, string[]>();

              foreach (var customer in customers)
              {
                  List<WordTransformationModel> transformationWordsList = new WordTransformationProvider().SelectAllItems(customer.Id);
                  List<FrequentWordModel> frequentWords = new FrequentWordProvider().SelectAllItems(customer.Id);

                  string[] frequentWordList = frequentWords.Select(p => p.Text).ToArray();

                  frequentWordArrayByCustomer.Add(customer.Id, frequentWordList);

                  var groupedList = transformationWordsList.GroupBy(p => p.FromWord);

                  java.util.HashMap map = new java.util.HashMap();
                  foreach (var groupItem in groupedList)
                  {
                      string key = groupItem.Key;

                      foreach (var item in groupItem)
                      {
                          map.put(key, item.ToWord);
                      }
                  }

                  reservedWordDataHashMapByCustomer.Add(customer.Id, map);
                  randomCustomerId = customer.Id;
              }

              GrammarManager.SetFrequentWordArrayDictionary(frequentWordArrayByCustomer);
              GrammarManager.SetReservedWordDataHashMapDictionary(reservedWordDataHashMapByCustomer);

              GrammarManager.Instance.ParseSentence("başla", randomCustomerId);
          }
      }
      public static void RestartGrammarManager()
      {
          GrammarManager.Dispose();
          InitializeGrammarManager();
      }
      public static void RestartBayesianManager()
      {
          BayesianManager.Dispose();
          var q = BayesianManager.Instance;
      }
      public static void RestartSystem()
      {
          RestartGrammarManager();
          RestartBayesianManager();
      }


      public static void RestartSystem(int customerId)
      {
          try
          {
              RestartGrammarManager(customerId);
              RestartBayesianManager(customerId);
              LogHelper.doLog_RethinkDB("System restart completed! CID: " + customerId, true);
          }
          catch (Exception ex)
          {
              LogHelper.doLog_RethinkDB("System restart ERROR: " + ex.ToString(), true);
          }
      }
      public static void InitializeGrammarManager(int customerId)
      {
          if (!GrammarManager.IsInitialized)
          {
              InitializeGrammarManager();
          }
          else
          {
              Dictionary<int, java.util.HashMap> reservedWordDataHashMapByCustomer = GrammarManager.GetReservedWordDataHashMapDictionary();
              Dictionary<int, string[]> frequentWordArrayByCustomer = GrammarManager.GetFrequentWordArrayDictionary();

              List<WordTransformationModel> transformationWordsList = new WordTransformationProvider().SelectAllItems(customerId);
              List<FrequentWordModel> frequentWords = new FrequentWordProvider().SelectAllItems(customerId);

              string[] frequentWordList = frequentWords.Select(p => p.Text).ToArray();

              if (frequentWordArrayByCustomer.ContainsKey(customerId))
              {
                  frequentWordArrayByCustomer[customerId] = frequentWordList;
              }
              else
              {
                  frequentWordArrayByCustomer.Add(customerId, frequentWordList);
              }

              var groupedList = transformationWordsList.GroupBy(p => p.FromWord);

              java.util.HashMap map = new java.util.HashMap();
              foreach (var groupItem in groupedList)
              {
                  string key = groupItem.Key;

                  foreach (var item in groupItem)
                  {
                      map.put(key, item.ToWord);
                  }
              }

              if (reservedWordDataHashMapByCustomer.ContainsKey(customerId))
              {
                  reservedWordDataHashMapByCustomer[customerId] = map;
              }
              else
              {
                  reservedWordDataHashMapByCustomer.Add(customerId, map);
              }

              GrammarManager.SetFrequentWordArrayDictionary(frequentWordArrayByCustomer);
              GrammarManager.SetReservedWordDataHashMapDictionary(reservedWordDataHashMapByCustomer);
          }
      }
      public static void RestartGrammarManager(int customerId)
      {
          InitializeGrammarManager(customerId);
      }
      public static void RestartBayesianManager(int customerId)
      {
          BayesianManager.Instance.Train(customerId);
      }
          
    }
}
