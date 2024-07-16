
--테이블 삭제
DROP TABLE EMP_DLL;
--p313 테이블 만들기
CREATE TABLE EMP_DLL (
    EMPNO   NUMBER(4),
    ENAME   VARCHAR2(10),
    JOB     VARCHAR2(9)
);
--검색
SELECT * FROM EMP_DLL;
--삽입
INSERT INTO EMP_DLL
VALUES (1, 'TOM', 'MANAGER');
INSERT INTO EMP_DLL
VALUES (2, '홍길동', '대리');
--검색
SELECT * FROM EMP_DLL;
-------------------------------------------------
-- 학생 테이블 만들기
CREATE TABLE student (
    id      number(4),
    name    varchar2(20),
    hp      varchar2(13),
    location varchar2(50)
);
-- 학생 테이블 구조 보기
DESC student;
-- 검색
SELECT * FROM STUDENT;
--삽입(INSERT)
INSERT INTO STUDENT (id, name, hp, location)
VALUES (1, '홍길동', '010-1111-2222', '안동');

INSERT INTO STUDENT (id, name, hp, location)
VALUES (1, '이순신', '010-2222-3333', '대구');

--SQL 학습
SELECT * FROM EMP;
--중복 제거 및 오름차순으로 검색해 주세요.
SELECT DISTINCT DEPTNO FROM EMP ORDER BY DEPTNO;
--급여가 높은순으로 이름과 급여를 출력해 주세요. 정렬은 급여를 기준으로 내림차순으로 정렬해주세요.
SELECT ENAME, JOB, SAL FROM EMP ORDER BY SAL DESC;

-- 4-14 부서번호(오름차순), 급여(내림차순)
SELECT * FROM EMP;

SELECT * FROM EMP
ORDER BY DEPTNO ASC, SAL DESC;

SELECT * FROM EMP
ORDER BY SAL DESC, DEPTNO ASC;

--연습문제 P92
--Q2
SELECT DISTINCT JOB FROM EMP; -- DISTINCT는 중복 제거

--EMP테이블에서 부서번호가 30인 데이터만 호출해 봅시다.
SELECT * FROM EMP
WHERE DEPTNO = 30;
-- EMP 테이블에서 직업이 'MANAGER'인 데이터만 호출해 봅시다.
SELECT * FROM EMP
WHERE JOB = 'MANAGER';
--직업이 'MANAGER' 부서번호 30 인 사원을 출력해 보세요.
SELECT * FROM EMP
WHERE JOB = 'MANAGER' AND DEPTNO = 30;
--부서번호가 30번인 모든 사람과 JOB이 'CLERK'인 모든 사람을 출력하세요
SELECT * FROM EMP
WHERE JOB = 'CLERK' OR DEPTNO = 30;

--산술연산-- 실습 5-5
SELECT * FROM EMP
WHERE SAL * 12 =36000;
--급여가 $3000 이상인 직원을 모두 출력하라
SELECT * FROM EMP WHERE SAL >= 3000;

--급여가 2500 이상이고 직업이 'ANALYST' 인 사원은??
SELECT * FROM EMP
WHERE SAL >= 2500 AND JOB = 'ANALYST';

--알파벳의 크기(?)
SELECT * FROM EMP
WHERE ENAME >= 'F' ORDER BY ENAME;

--급여가 3000이 아닌 모든 데이터 출력
SELECT * FROM EMP
WHERE SAL != 3000;
--위와 같다
SELECT * FROM EMP
WHERE SAL <> 3000;
--위와 같다
SELECT * FROM EMP
WHERE SAL ^= 3000;
--위와 같다
SELECT * FROM EMP
WHERE NOT SAL = 3000;

--IN 연산자
SELECT * FROM EMP
WHERE JOB = 'MANAGER' OR JOB = 'SALESMAN' OR JOB = 'CLERK';

SELECT * FROM EMP
WHERE JOB NOT IN ('MANAGER', 'SALESMAN', 'CLERK');

--급여가 2000이상 3000이하
SELECT * FROM EMP
WHERE SAL >= 2000 AND SAL <= 3000;
--BETWEEN A AND B
SELECT * FROM EMP
WHERE SAL BETWEEN 2000 AND 3000;
--------------------------------------------------
using Oracle.ManagedDataAccess.Client;

namespace ConsoleApp16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. 외부 프로그램 연결 모듈 받기 -- 도구 ->Nuget
            //2. 연결 스크립트를 사용
            string strConn = "Data Source=(DESCRIPTION=" +
                "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
                "(HOST=localhost)(PORT=1521)))" +
                "(CONNECT_DATA=(SERVER=DEDICATED)" +
                "(SERVICE_NAME=xe)));" +
                "User Id=scott;Password=tiger;";

            //1. 연결 객체 만들기
            OracleConnection conn = new OracleConnection(strConn);
            //2. 연결 객체 만들기 --> client
            conn.Open();
            //3. 프로그래밍
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            cmd.CommandText = "Create Table PhoneBook " +
                "(ID number(4) PRIMARY KEY,  " +
                "NAME varchar(20), " +
                "HP varchar(20))";
            cmd.ExecuteNonQuery();
            //4. 리소스 반환 및 종료
            conn.Close();
        }
    }
}
