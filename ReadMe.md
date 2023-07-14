

# web
进入 WebUi 目录

修改：src\setupProxy.js
    
    target: "https://XXXXXXX", // 后台服务地址以及端口号

修改：src\http\index\index.ts
    
    baseURL: "https://xxxxxxxxxxxxxx",



执行

    npm i
    npm run dev



# API
进入 WebAPI 目录
vs2022 .netcore 6.0

修改：Middlewares\RedisCacheMiddleware.cs Redis连接串
 
    return ConnectionMultiplexer.Connect("XXXXX:6379,password=XXXXX,allowadmin=true")

