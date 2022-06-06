# C#을 사용한 비동기 소켓 연습

## Preview

![preview](./misc/preview.png)

## 주절주절

1. `Server.cs`, `Client.cs`는 `SocketAsyncEventArgs`를 사용하고 있다. 이것은 `이벤트 기반 비동기 패턴(EAP)` 라고 하고, JavaScript의 Event와 동일하다.

2. `ServerTask.cs`, `ClientTask.cs`는 `Task` 기반, 즉 `작업 기반 비동기 패턴(TAP)`을 사용한다.

3. 역시 고급언어라 그런지 코드가 굉장히 간단함.

4. 버퍼가 1024 bytes인데 10240 bytes 짜리 메시지를 보내면 터지지 않고 알아서 나뉘어 온다. (내부적으로 잘 하는 듯?)
