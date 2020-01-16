namespace BT.Manage.Core.SignatureVerify.Base
{
    /// <summary>
    /// 接口校验返状态(返回码)
    /// </summary>
    public enum StatusCodeEnum
    {
        //token参数格式有误
        StaffIdError = 105152,
        //成功
        Success = 000000,
        //签名校验失败
        SignatureFailure = 105145,
        //缺少签名( SignatureDeleTion)参数或参数为空
        SignatureDeleTion = 105155,
        //缺少时间戳(TimeSpanDeletion)参数或参数为空
        TimeSpanDeletion = 105164,
        //时间戳格式有误
        TimeSpanError = 105146,
        //请求已超时
        TimeSpanTimeOut = 105161,
    }
}
