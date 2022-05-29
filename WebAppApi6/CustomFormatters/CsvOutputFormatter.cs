using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Collections;
using System.Text;
using WebAppApi6.Models;

namespace WebAppApi6.CustomFormatters
{

    public static class CsvOutputFormatterSetupHelper
    {
        public static IMvcBuilder AddCsvOutputFormatter(this IMvcBuilder builder)
        {
            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, CsvOutputFormatterSetup>()
                );
            return builder;
        }
    }


    public class CsvOutputFormatterSetup : IConfigureOptions<MvcOptions>
    {
        void IConfigureOptions<MvcOptions>.Configure(MvcOptions options)
        {
            options.OutputFormatters.Add(new CsvOutputFormatter());
        }
    }


    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/csv"));
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(Book).IsAssignableFrom(type) || typeof(IEnumerable<Book>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<Book>)
            {
                //foreach (var o in (IEnumerable<Book>)context.Object)
                //{
                //	FormatCsv(buffer, o);
                //}

                FormatCsv(buffer, (IEnumerable<Book>)context.Object);

                using (var writer = context.WriterFactory(response.Body, selectedEncoding))
                {
                    await writer.WriteAsync(buffer.ToString());
                }
            }

            await Task.Delay(0);
        }

        private static void FormatCsv(StringBuilder buffer, IEnumerable<Book> books)
        {
            foreach (var b in books)
            {
                buffer.AppendLine($"{b.Title},\"{b.Description},\"{b.AmountOfPages},\"{b.AuthorId}\"");
            }
        }
    }

    public class CsvMediaTypeFormatterV2 : TextOutputFormatter
    {

        /// <summary>
        /// CSV Formatter
        /// </summary>
        public CsvMediaTypeFormatterV2()
        {
            SupportedMediaTypes.Add(Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        /// <summary>
        /// Write the response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="selectedEncoding"></param>
        /// <returns></returns>
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var csv = new StringBuilder();
            Type type = GetTypeOf(context.Object);

            csv.AppendLine(
                string.Join<string>(
                    ",", type.GetProperties().Select(x => x.Name)
                )
            );

            foreach (var obj in (IEnumerable<object>)context.Object)
            {
                var vals = obj.GetType().GetProperties().Select(
                    pi => new
                    {
                        Value = pi.GetValue(obj, null)
                    }
                );

                List<string> values = new List<string>();
                foreach (var val in vals)
                {
                    if (val.Value != null)
                    {
                        var tmpval = val.Value.ToString();

                        //Check if the value contans a comma and place it in quotes if so
                        if (tmpval.Contains(","))
                            tmpval = string.Concat("\"", tmpval, "\"");

                        //Replace any \r or \n special characters from a new line with a space
                        tmpval = tmpval.Replace("\r", " ", StringComparison.InvariantCultureIgnoreCase);
                        tmpval = tmpval.Replace("\n", " ", StringComparison.InvariantCultureIgnoreCase);

                        values.Add(tmpval);
                    }
                    else
                    {
                        values.Add(string.Empty);
                    }
                }
                csv.AppendLine(string.Join(",", values));
            }
            return context.HttpContext.Response.WriteAsync(csv.ToString(), selectedEncoding);
        }

        private static Type GetTypeOf(object obj)
        {
            Type type = obj.GetType();
            Type itemType;
            if (type.GetGenericArguments().Length > 0)
            {
                itemType = type.GetGenericArguments()[0];
            }
            else
            {
                itemType = type.GetElementType();
            }
            return itemType;
        }

        protected override bool CanWriteType(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

    }

}
