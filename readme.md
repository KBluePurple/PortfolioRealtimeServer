# 포트폴리오용 실시간 서버

Websocket + Flatbuffers + C#

커플링을 최소화하며 객체지향적으로 개발하고자 노력했습니다. 최대한 본인의 실력으로 개발하며 성장하기 위하여 시작하는 프로젝트입니다.

### Roadmap
- [x] 세션
- [x] Flatbuffers 패킷 처리
- [x] Heartbeat 처리
- [ ] ZSTD를 이용한 패킷 압축
- [ ] 일시적 연결 끊김 시 세션 복구
- [ ] 채팅
- [ ] 캐릭터 움직임
- [ ] 캐릭터 액션
- [ ] DB 연동
- [ ] 로그인
- [ ] 인벤토리
- [ ] TCP 서버로 전환

![image](https://user-images.githubusercontent.com/42506968/232533931-aa35818e-aef2-4c5d-9e7b-5ffd2dbeae54.png)

### Dev Log
**2023-04-18**: 첫 개발 시작
- 세션 구현
- Flatbuffers 패킷 처리
- Heartbeat 처리

![image](https://user-images.githubusercontent.com/42506968/236685604-21901ee6-51c2-4d80-bf56-a3101e2365a2.gif)

**2023-05-07**: HeartHeat 서비스 개편
- 로그 파일 기록 기능
- 기존 Heartbeat는 1초마다 모두에게 한번에 보내는 방식이었음
- 이는 세션 수가 많아질수록 성능 저하를 가져옴
- 이를 개선하기 위해 Heartbeat 서비스를 개편함
- 서버에 Tick을 두고, Session 마다 타이머를 두어 시간을 측정함
- Tick이 지날 때마다 Session의 타이머를 감소시킴
- 타이머가 0이 되면 해당 Session에게 Heartbeat 패킷을 보냄
