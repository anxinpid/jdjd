import request from "./index"

// 请求中： 请求参数和返回值的类型都需要进行约束

// 获取所有
// export const BaojiaGetAPI = () =>request.get("posts");
// export const BaojiaPostAPI = (params) =>request.post("posts",params);
// export const BaojiaPutAPI = (params) =>request.get("posts",params);
// export const BaojiaPatchAPI = (params) =>request.get("posts",params);
// export const BaojiaDeleteAPI = (params) =>request.get("posts",params);
// export const BaojiaOptionsAPI = (params) =>request.get("posts",params);


export const BaojiaGetAPI = () => request.get("api/Baojia");
export const BaojiaPostAPI = (params) => request.post("api/Baojia", params);
export const BaojiaPutAPI = (id, params) => request.put("api/Baojia/" + id, params);
export const BaojiaPatchAPI = (id, params) => request.patch("api/Baojia/" + id, params);
export const BaojiaDeleteAPI = (id, params) => request.delete("api/Baojia/" + id, params);
export const BaojiaOptionsAPI = (id, params) => request.options("api/Baojia/" + id, params);


