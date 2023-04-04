# EasyNotice

## Nuget包

| Package Name |  Version | Downloads
|--------------|  ------- | ----
| EasyNotice.Core | ![](https://img.shields.io/badge/nuget-v1.2.0-blue) | ![](https://img.shields.io/badge/downloads-xM-brightgreen)|
| EasyNotice.Dingtalk | ![](https://img.shields.io/badge/nuget-v1.2.0-blue) | ![](https://img.shields.io/badge/downloads-xM-brightgreen)|
| EasyNotice.Email | ![](https://img.shields.io/badge/nuget-v1.2.0-blue) | ![](https://img.shields.io/badge/downloads-xM-brightgreen)|

---------

# `EasyNotice`
> 这是一个基于.NET开源的消息通知组件，它包含了邮件通知、钉钉通知，可以帮助我们更容易地发送程序异常通知！

-------

## 功能介绍
 - 支持邮件发送、钉钉发送
 - 支持自定义发送间隔，避免同样的异常频繁通知
 - 傻瓜式配置，开箱即用


 ---------

# 项目接入

## 1. 发送邮件通知
> 邮件通知支持同时发送给多个收件人
### Step 1 : 安装包，通过Nuget安装包

```powershell
Install-Package EasyNotice.Core
Install-Package EasyNotice.Email
```

### Step 2 : 配置 Startup 启动类

```csharp
public class Startup
{
    //...
    
    public void ConfigureServices(IServiceCollection services)
    {
        //configuration
        services.AddEsayNotice(config =>
        {
            config.IntervalSeconds = 10;//同一标题的消息，10秒内只能发一条，避免短时间内大量发送重复消息
            config.UseEmail(option =>
            {
                option.Host = "smtp.qq.com";//smtp域名
                option.Port = 465;//端口
                option.FromName = "System";//发送人名字（自定义）
                option.FromAddress = "12345@qq.com";//发送邮箱
                option.Password = "passaword";//秘钥
                option.ToAddress = new List<string>()//收件人集合
                {
                    "12345@qq.com"
                };
            });
        });
    }    
}
```

### Step 3 : IEmailProvider服务接口使用

```csharp
[ApiController]
[Route("[controller]/[action]")]
public class NoticeController : ControllerBase
{
    private readonly IEmailProvider _mailProvider;
    public NoticeController(IEmailProvider provider)
    {
        _mailProvider = provider;
    }

    [HttpGet]
    public async Task SendMail([FromQuery] string str)
    {
        //发送邮件
        await _mailProvider.SendAsync(str, new Exception(str));
    }
}
```
---------


## 2. 发钉钉通知
### Step 1 : 安装包，通过Nuget安装包

```powershell
Install-Package EasyNotice.Core
Install-Package EasyNotice.Dingtalk
```

### Step 2 : 配置 Startup 启动类

```csharp
public class Startup
{
    //...
    
    public void ConfigureServices(IServiceCollection services)
    {
        //configuration
        services.AddEsayNotice(config =>
        {
            config.IntervalSeconds = 10;//同一标题的消息，10秒内只能发一条，避免短时间内大量发送重复消息
            config.UseDingTalk(option =>
            {
                option.WebHook = "https://oapi.dingtalk.com/robot/send?access_token=xxx";
                option.Secret = "secret";
            });
        });
    }    
}
```

### Step 3 : IDingtalkProvider服务接口使用

```csharp
[ApiController]
[Route("[controller]/[action]")]
public class NoticeController : ControllerBase
{
    private readonly IDingtalkProvider _dingtalkProvider;
    public NoticeController(IDingtalkProvider dingtalkProvider)
    {
        _dingtalkProvider = dingtalkProvider;
    }

    [HttpGet]
    public async Task SendDingTalk([FromQuery] string str)
    {
        await _dingtalkProvider.SendAsync(str, new Exception(str));
    }
}
```

---------
## 更多示例

1. 查看 [更多使用例子](https://github.com/Bryan-Cyf/EasyNotice/tree/master/sample)
2. 查看 [更多测试用例](https://github.com/Bryan-Cyf/EasyNotice/tree/master/test)

