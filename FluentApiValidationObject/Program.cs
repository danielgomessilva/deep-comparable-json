using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace FluentApiValidationObject
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = File.ReadAllText(GetPath("object.json"));
            var toBeCompared = File.ReadAllText(GetPath("toBeCompared.json"));

            var result = obj.Compare(toBeCompared)
                .AddField("codigoInternoParceiro")
                .AddField("nome")
                .ThenAddInclude("enderecoEntrega")
                    .AddField("cep")
                    .AddField("logradouro")
                    .ThenAddInclude("enderecoEntrega")
                    .BackLevel<Comparable>()
                .AddListField("itens")
                    .AddToPosition(0)
                        .AddField("item")
                        .AddField("quantidade")
                        .BackLevel<ListComparable>()
                    .AddToPosition(1)
                        .AddField("item")
                        .AddField("quantidade")
                .IsEquivalent;
        }

        private static string GetPath(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "exemplo", fileName).ToString();
        }
    }
    public static class Helper
    {
        public static Comparable Compare(this object obj, object toBeCompared)
        {
            return new Comparable(obj, toBeCompared);
        }
    }
    
    
}
