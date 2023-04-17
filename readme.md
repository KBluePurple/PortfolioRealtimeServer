# 포트폴리오용 실시간 서버

Websocket + Flatbuffers + C#

제대로 사용할 포폴이 갈고닦은 기술에 비해 너무 부족하다 판단하여 개발하기 시작한 프로젝트입니다.

커플링을 최소화하며 객체지향적으로 개발하고자 노력했습니다. 최대한 본인의 실력으로 개발하며 성장하기 위하여 함수를 통째로 복사하는 등의 날먹 코딩을 하지 않았습니다.

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
