using Microsoft.Extensions.Configuration;

namespace WebApp01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://jd.tyinvoice.cn", "https://jd.tyinvoice.cn", "http://127.0.0.1:5173")
                                        .WithExposedHeaders("x-custom-header")//���ù�������Ӧͷ
                                        .AllowAnyHeader()//������������ͷ
                                        .AllowAnyMethod()//�����κη���
                                        .AllowCredentials()//�����Դƾ��----��������������ƾ��
                                        .SetIsOriginAllowed(_ => true);
                                      ;
                                  });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddStackExchangeRedisCacheSetup();






            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }
    }
}