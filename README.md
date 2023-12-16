## 0.前言
最近在做CS架构的上位机控制软件，服务端和客户端是通过zmq进行通讯的，网上现有的工具都是tcp、串口的调试工具，一直没有找到一个合适的zmq调试工具。就使用C#语言开发了这个简易的zmq调试工具，项目地址
## 1.主要功能
 1. zmq4种通讯模式
 2. 通过订阅主题进行订阅数据筛选，主要机制是删选关键字
 3. 选择Text和Hex两种不同发送和接收模式
![在这里插入图片描述](https://img-blog.csdnimg.cn/direct/79ff641ed0fc4d30a871c212f29d137a.png)

2.配置文件
appsetting.json
```xml
{
  "RequestAddress": "tcp://localhost:3000",
  "RequestOrders": [
    {
      "Title": "设置参数_单次测量",
      "Message": {
        "theme": "setMeasureParams",
        "param": {
          "measureMode": "单次测量",
          //样品信息，为了和其它命名一致的妥协结果
          "sampleInformation": "JGSC-BTF",
          //采样次数
          "sampleCount": 100
        }
      }
    },
    {
      "Title": "设置参数_采集光谱",
      "Message": {
        "theme": "setMeasureParams",
        "param": {
          "measureMode": "采集光谱",
          //样品信息，为了和其它命名一致的妥协结果
          "sampleInformation": "JGSC-BTF",
          //采样次数
          "sampleCount": 100
        }
      }
    },
    {
      "Title": "启动测量",
      "Message": {
        "theme": "measureControl",
        //startMeasure stopMeasure
        "param": "startMeasure"
      }
    },
    {
      "Title": "停止测量",
      "Message": {
        "theme": "measureControl",
        //startMeasure stopMeasure
        "param": "stopMeasure"
      }
    }
  ],
  "ResponseAddress": "tcp://*:3000",
  "ResponseOrders": [
    {
      "Title": "准备测量",
      "Message": "120 156 99 96 0 3 190 192 99 145 236 185 222 19 207 51 232 129 32 0 38 145 4 40"
    },
    {
      "Title": "更新进度",
      "Message": "120 156 99 96 96 96 116 16 97 0 3 0 2 161 0 86"
    },
    {
      "Title": "本次测量结束",
      "Message": "120 156 99 96 0 3 190 116 157 108 197 92 239 137 231 235 46 167 199 255 103 4 0 44 22 6 93"
    }
  ],
  "SubscriberAddress": "tcp://localhost:2000",
  "SubscriberOrders": [
    {
      "Title": "准备测量",
      "Message": "120 156 99 96 0 3 190 192 99 145 236 185 222 19 207 51 232 129 32 0 38 145 4 40"
    },
    {
      "Title": "更新进度",
      "Message": "120 156 99 96 96 96 116 16 97 0 3 0 2 161 0 86"
    },
    {
      "Title": "本次测量结束",
      "Message": "120 156 99 96 0 3 190 116 157 108 197 92 239 137 231 235 46 167 199 255 103 4 0 44 22 6 93"
    }
  ],
  "PublisherAddress": "tcp://*:2000",
  "PublishOrders": [
    {
      "Title": "准备测量",
      "Message": "120 156 99 96 0 3 190 192 99 145 236 185 222 19 207 51 232 129 32 0 38 145 4 40"
    },
    {
      "Title": "更新进度",
      "Message": "120 156 99 96 96 96 116 16 97 0 3 0 2 161 0 86"
    },
    {
      "Title": "本次测量结束",
      "Message": "120 156 99 96 0 3 190 116 157 108 197 92 239 137 231 235 46 167 199 255 103 4 0 44 22 6 93"
    }
  ]
}
```
通过修改不同值进行地址和发送参数的配置
