using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 이미 작성된 코드에서 수정&보완 해야할거
 *  1. 플레이어 공격, 몬스터 공격 판정 수정(여러명이 동시에 Collider에 진입했을 시를 고려해서 List등을 사용)
 *  2. 플레이어 피격 시 플레이어 행동 로직 취소(공격 모션 도중 공격 받으면 공격 취소, 방패를 반대로 들었을 때 피격 시 방패 상태 취소)
 *  3. 몬스터 피격 시 몬스터 행동 로직 수정(공격 모션 도중 공격 받으면 공격 취소, 슈퍼아머 공격 추가)
 *  4. 점프 공격 시 Velocity 조정
 *  5. Stat을 통해 공격에 계수를 적용시키기
 *  6. 몬스터 처치 시 경험치 및 골드 획득
 *  7. Weapon에 플레이어의 Core를 받아와서 응집도 낮추기
 *  8. Weapon에 각 State를 받아오는 방식에서 별도로 분리해서 응집도 낮추기 -> 실패
 *  9. 패링 성공 시 패링 키 Hold 상태면 다시 패링 상태로 가는 현상 수정
 *  10. 공격을 사용했을 시 공격을 중간에 취소하고 패링을 할 수 있는 상태로 전환하는 구간 수정
 *  11. 몬스터 패링 및 공격 후 넉백 단계에서 바닥 끝 부분이면 떨어지지 않게 하기
 *  12. 플레이어가 몬스터 못 밀게 하




 * 추가로 작성&고려 해야할거
 *  1. 패링 성공 시 적에게 상태이상 부여(기절 등)
 *  2. 스킬/아이템 사용 시 효과 적용(공격 속도, 이동 속도, 공격력 등)
 *  3. 맵 환경을 통한 효과 적용(공격 속도, 이동 속도 등)
 *  4. Stat 클래스를 몬스터,플레이어 별도로 분리
 *  5. 패링 성공 시 추가 공격로직 구현
 *  6. Json 파일을 통해 플레이어 성장 수치 작성
 *  7. 입력을 플레이어 입력 / UI입렷 분리
 *  8. 최상위 State생성(일시정지상태, 진행상태)
 *  9. 데이터를 어떻게 뽑아올 것인가 (전투중일 때 / 플레이어 무기 종류 / n초당 이동패턴 / 공격 주기 / 패링 횟수 / 패링 성공 확률 / 패링 전-후 행동패턴 / 회피 횟수 / 회피 성공 확률 / 회피 전-후 행동패턴)
 *  10. Monster ObjectPool 작성
 *  11. Monster Spowner 작성
 *  12. Dead 관련 플레이어 / 몬스터 코드 작성
 *  13. Damage와 같은 여러 변수가 있는 로직 최종적으로 계산하는 클래스 구현 (적 공격 -> 플레이어 방어력 + 속성을 통한 데미지 감소/증가 + 스킬 및 아이템 사용을 통한 데미지 감소 + 적의 추가 공격 강화 효과)

 */