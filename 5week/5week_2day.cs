DECLARE
    V_NUM NUMBER := 0;
BEGIN
    WHILE V_NUM <= 10 LOOP
        IF MOD(V_NUM, 2) = 1 THEN
            DBMS_OUTPUT.PUT_LINE(V_NUM);
        END IF;
        V_NUM := V_NUM + 1;
    END LOOP;
END;

BEGIN
    FOR i IN 0..10 LOOP
        IF MOD(i, 2) = 1 THEN
            DBMS_OUTPUT.PUT_LINE(i);
        END IF;
    END LOOP;
END;

-----------------------------------------------------
--제약 중복되지 않는 값 unique
--14-1
CREATE TABLE TABLE_UNIQUE(
    LOGIN_ID VARCHAR2(20)      UNIQUE,
    LOGIN_PWD VARCHAR2(20)     NOT NULL,
    TEL     VARCHAR2(20)
);

SELECT * FROM TABLE_UNIQUE;

--14-15
SELECT OWNER, CONSTRAINT_NAME, CONSTRAINT_TYPE, TABLE_NAME FROM USER_CONSTRAINTS WHERE TABLE_NAME = 'TABLE_UNIQUE';

--14-16
INSERT INTO TABLE_UNIQUE (LOGIN_ID, LOGIN_PWD, TEL)
VALUES ('TEST_ID_01', 'PWD01', '010-12340-5678');

CREATE TABLE TABLE_UNIQUE2(
    LOGIN_ID VARCHAR2(20) CONSTRAINT TBLUNQ2_LGNID_UNQ UNIQUE,
    LOGIN_PWD VARCHAR2(20) CONSTRAINT TBLUNQ2_LGNPW_NN NOT NULL,
    TEL     VARCHAR2(20)
);
SELECT OWNER, CONSTRAINT_NAME, CONSTRAINT_TYPE, TABLE_NAME FROM USER_CONSTRAINTS WHERE TABLE_NAME LIKE 'TABLE_UNIQUE%';

--제약조건 삭제하기
ALTER TABLE TABLE_UNIQUE2
DROP CONSTRAINT TBLUNQ2_LGNPW_NN;

--14-29
CREATE TABLE TABLE_PK(
    LOGIN_ID VARCHAR2(20) PRIMARY KEY,
    LOGIN_PWD VARCHAR2(20) NOT NULL,
    TEL VARCHAR2(20)
);

SELECT OWNER, CONSTRAINT_NAME, CONSTRAINT_TYPE, TABLE_NAME FROM USER_CONSTRAINTS WHERE TABLE_NAME LIKE 'TABLE_PK%';

--INDEX 확인
SELECT INDEX_NAME, TABLE_OWNER, TABLE_NAME FROM USER_INDEXES WHERE TABLE_NAME LIKE 'TABLE_PK%';

--14-35
CREATE TABLE TABLE_PK2(
    LOGIN_ID VARCHAR2(20) CONSTRAINT TBLPK2_LGNID_PK PRIMARY KEY,
    LOGIN_PWD VARCHAR2(20) CONSTRAINT TBLPK2_LGNPW_NN NOT NULL,
    TEL VARCHAR2(20)
);
DESC TABLE_PK2;

--삽입 14-33
INSERT INTO TABLE_PK
VALUES ('TEST_ID_01', 'PWD01', '010-1234-5678');

INSERT INTO TABLE_PK
VALUES ('TEST_ID_02', NULL, '010-1234-5678');
SELECT * FROM TABLE_PK;

--TALBE 작성 방법
CREATE TABLE TABLE_PK(
    LOGIN_ID VARCHAR2(20),
    LOGIN_PWD VARCHAR2(20),
    TEL VARCHAR2(20),
    PRIMARY KEY(LOGIN_ID) --제약조건을 밑으로 따로 빼서 만들수 도 있다.
);

SELECT * FROM EMP;
SELECT * FROM DEPT;
--실습 14-37 테이블간의 제약조건 살펴보기
SELECT OWNER, CONSTRAINT_NAME, CONSTRAINT_TYPE, TABLE_NAME, R_OWNER
FROM USER_CONSTRAINTS WHERE TABLE_NAME IN ('EMP', 'DEPT');

--테이블 간의 관계 연관(R) 만들기
--14-29
CREATE TABLE DEPT_FK(
    DEPTNO      NUMBER(2) CONSTRAINT DEPTFK_DEPTNO_PK PRIMARY KEY,
    DNAME       VARCHAR2(14),
    LOC         VARCHAR2(13)
);
SELECT * FROM DEPT_FK;

INSERT INTO DEPT_FK
VALUES (10, '개발1팀', '안동');

DELETE DEPT_FK WHERE DEPTNO = 20;

CREATE TABLE EMP_FK(
    EMPNO       NUMBER(4) CONSTRAINT EMPFP_EMPNO_PK PRIMARY KEY,
    ENAME       VARCHAR2(10),
    DEPTNO      NUMBER(2) CONSTRAINT EMPFK_DEPTNOO_PK REFERENCES DEPT_FK(DEPTNO)ON DELETE CASCADE
);
DROP TABLE EMP_FK;
DROP TABLE DEPT_FK;
INSERT INTO EMP_FK
VALUES (1, '이순신', 10);
DELETE EMP_FK WHERE DEPTNO = 20;
SELECT * FROM EMP_FK;

--14-45 테이블 생성 시  CHECK 제약 조건 설정하기
CREATE TABLE TABLE_CHECK(
    LOGIN_ID    VARCHAR2(20) PRIMARY KEY,
    LOGIN_PWD   VARCHAR2(20) CHECK (LENGTH(LOGIN_PWD) > 3), -- 3길이 이상 제약주기
    TEL         VARCHAR2(20)
);

INSERT INTO TABLE_CHECK
VALUES ('TEST_ID1', '12345', '010-1234-5678');
SELECT * FROM TABLE_CHECK;

--기본값을 정하는 DEFAULT
--14-49
CREATE TABLE TABLE_DEFAULT(
    LOGIN_ID VARCHAR2(20) PRIMARY KEY,
    LOGIN_PW VARCHAR2(20) DEFAULT '1234',
    TEL      VARCHAR2(20)
);
SELECT * FROM TABLE_DEFAULT;

DROP TABLE TABLE_DEFAULT;

INSERT INTO TABLE_DEFAULT
VALUES ('TEST_IN_01', 'ABCD', '010-1111-1111');

INSERT INTO TABLE_DEFAULT
VALUES ('TEST_ID_02', NULL, '010-2222-2222');

INSERT INTO TABLE_DEFAULT (LOGIN_ID)
VALUES ('TEST_ID_03');

INSERT INTO TABLE_DEFAULT (TEL, LOGIN_PW, LOGIN_ID)
VALUES ('010-333-3333', '5678', 'MYID');

--P394 Q1
CREATE TABLE DEPT_CONST(
    DEPTNO      NUMBER(2) CONSTRAINT DEPTCONST_DEPTNO_PK PRIMARY KEY,
    DNAME       VARCHAR2(14) CONSTRAINT DEPTCONST_DNAME_UNQ UNIQUE,
    LOC         VARCHAR2(13) CONSTRAINT DEPTCONST_LOC_NN NOT NULL
);
SELECT * FROM DEPT_CONST;
SELECT 


-----------------------------------------------------
SELECT * FROM HELP;

CREATE USER ORCLSTUDY
IDENTIFIED BY ORACLE;

GRANT CREATE SESSION TO ORCLSTUDY;

SELECT * FROM ALL_USERS -- 오라클에서 만들어진 사용자 테이블
WHERE USERNAME = 'ORCLSTUDY';

SELECT * FROM DBA_OBJECTS;

--사용자 삭제
DROP USER ORCLSTUDY CASCADE;
COMMIT;

--15-7
CREATE  USER ORCLSTUDY IDENTIFIED BY ORACLE;

GRANT RESOURCE, CREATE SESSION, CREATE TABLE TO ORCLSTUDY;

REVOKE RESOURCE FROM ORCLSTUDY;

SELECT * FROM ALL_USERS;

--ROLE 사용 
--15-14
--여러권한을 한번에 부여하려면  ROLE을 만들어야한다.
CREATE ROLE ROLESTUDY;
GRANT CONNECT, RESOURCE, CREATE VIEW, CREATE SYNONYM TO ROLESTUDY;

--한번에 권한 부여
GRANT ROLESTUDY TO ORCLSTUDY;
--한번에 권힌 제거
REVOKE ROLESTUDY FROM ORCLSTUDY;
--계정과 권한 함께 제거
DROP USER ORCLSTUDY CASCADE;

-----------------------------------------------------
  //오라클 코딩
--PL/SQL
--16-1
SET SERVEROUTPUT ON; --실행 결과를 화면에 출력
BEGIN
    DBMS_OUTPUT.PUT_LINE('HELLO, PL/SQL!');
END;

--한줄 주석 사용하기

DECLARE
 V_EMPNO NUMBER(4) := 7788;
 V_ENAME VARCHAR2(10);
 BEGIN
 V_ENAME := 'SCOTT';
 DBMS_OUTPUT.PUT_LINE('V_EMPNO : ' || V_EMPNO);
 DBMS_OUTPUT.PUT_LINE('V_ENAME : ' || V_ENAME);
 END;
 
 DECLARE
    DEPTNO  NUMBER(10);
    DNAME   VARCHAR2(10);
    LOC     VARCHAR2(10);
    BEGIN
    DEPTNO := 10;
    DNAME := '홍길동';
    LOC := '안동';
    DBMS_OUTPUT.PUT_LINE('DEPTNO : ' || DEPTNO);
    DBMS_OUTPUT.PUT_LINE('DNAME : ' || DNAME);
    DBMS_OUTPUT.PUT_LINE('LOC : ' || LOC);
    END;
    
DECLARE
    V_TAX CONSTANT NUMBER(1) := 3;
BEGIN
    DBMS_OUTPUT.PUT_LINE('V_TAX : ' || V_TAX);
END;

--PL/SQL 변수의 기본값을 설정할 수 있다.
DECLARE
    V_DEPTNO NUMBER(2) DEFAULT 10;
BEGIN
    --V_DEPTNO := 50;
    DBMS_OUTPUT.PUT_LINE(V_DEPTNO);
END;

--Q변수에 NOT NULL을 설정하고 값을 대입한 후 출력
DECLARE
    V_DEPTNO NUMBER(2) NOT NULL := 20;
BEGIN
    DBMS_OUTPUT.PUT_LINE(V_DEPTNO);
END;

--스칼라형 변수

--참조형 변수
--16-9 참조형의 변수에 값을 대입한 후 출력하기 %TYPE
DECLARE
    V_DEPTNO DEPT.DEPTNO%TYPE DEFAULT 50;
BEGIN
    DBMS_OUTPUT.PUT_LINE('V_DEPTNO : ' || V_DEPTNO);
END;

--%ROWTYPE
DECLARE
    V_DEPT_ROW DEPT%ROWTYPE;
BEGIN
    SELECT DEPTNO, DNAME, LOC INTO V_DEPT_ROW
    FROM DEPT
    WHERE DEPTNO = 40;
    DBMS_OUTPUT.PUT_LINE('DEPTNO : ' || V_DEPT_ROW.DEPTNO);
    DBMS_OUTPUT.PUT_LINE('DNAME : ' || V_DEPT_ROW.DNAME);
    DBMS_OUTPUT.PUT_LINE('LOC : ' || V_DEPT_ROW.LOC);
END;

DECLARE
    V_NUMBER NUMBER DEFAULT 13;
BEGIN
    IF MOD(V_NUMBER, 2) = 1 THEN
        DBMS_OUTPUT.PUT_LINE('홀수 입니다.');
    ELSE
        DBMS_OUTPUT.PUT_LINE('짝수 입니다.');
    END IF;
END;

--16-14 입력한 점수 어느 학점인지 출력
--16-15 CASE문 (switch case)
DECLARE
    V_SCORE NUMBER DEFAULT 10;
BEGIN
    CASE V_SCORE
        WHEN 10 THEN DBMS_OUTPUT.PUT_LINE('A학점');
        WHEN 8 THEN DBMS_OUTPUT.PUT_LINE('B학점');
        ELSE DBMS_OUTPUT.PUT_LINE('F학점');
    END CASE;
END;

--기본 LOOP사용하기
DECLARE
    V_NUM NUMBER := 0;
BEGIN
    LOOP
        DBMS_OUTPUT.PUT_LINE(V_NUM);
        V_NUM := V_NUM + 1;
        EXIT WHEN V_NUM > 4;
    END LOOP;
END;

--WHILE문 사용하기
DECLARE
    V_NUM NUMBER := 0;
BEGIN
    WHILE V_NUM < 4 LOOP
    DBMS_OUTPUT.PUT_LINE(V_NUM);
    V_NUM := V_NUM + 1;
    END LOOP;
END;

BEGIN
    FOR i IN 0..4 LOOP
        DBMS_OUTPUT.PUT_LINE(i);
    END LOOP;
END;
--P444 Q1
DECLARE
    V_NUM NUMBER := 0;
BEGIN
    WHILE V_NUM <= 10 LOOP
        IF MOD(V_NUM, 2) = 1 THEN
            DBMS_OUTPUT.PUT_LINE(V_NUM);
        END IF;
        V_NUM := V_NUM + 1;
    END LOOP;
END;

BEGIN
    FOR i IN 0..10 LOOP
        IF MOD(i, 2) = 1 THEN
            DBMS_OUTPUT.PUT_LINE(i);
        END IF;
    END LOOP;
END;
-----------------------------------------------------
//명함 만들기
using Oracle.ManagedDataAccess.Client;

namespace OracleTest1
{
    class A
    {
        public void Test()
        {
            Console.WriteLine("0. 테이블 생성 초기화 (삭제 후 만들기)");
            Console.WriteLine("1. 명함 추가");
            Console.WriteLine("2. 명함 목록 보기");
            Console.WriteLine("3. 명함 수정");
            Console.WriteLine("4. 명함 삭제");
            Console.WriteLine("5. 명함 검색");
            Console.WriteLine("6. 종료");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            string strConn = "Data Source=(DESCRIPTION=" +
                "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
                "(HOST=localhost)(PORT=1521)))" +
                "(CONNECT_DATA=(SERVER=DEDICATED)" +
                "(SERVICE_NAME=xe)));" +
                "User Id=scott;Password=tiger;";
            //1. 연결 객체 만들기 - client
            OracleConnection conn = new OracleConnection(strConn);

            //2.데이터베이스 접속을 위한 연결
            conn.Open();

            //3.서버와 함께 신나게 놀기

            //3.1 Query 명령객체 만들기
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn; //연결객체와 연동

            A test = new A();
            string seq = "seq_businesscards.nextval";
            while (true)
            {
                test.Test();
                int num;
                int end = 0;
                
                switch (num = int.Parse(Console.ReadLine()))
                {
                    case 0:
                        //명함 테이블 삭제
                        cmd.CommandText = "DROP TABLE BUSINESSCARDS";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "DROP SEQUENCE SEQ_BUSINESSCARDS";
                        cmd.ExecuteNonQuery();
                        //명함 테이블 제작
                        cmd.CommandText = "CREATE TABLE BusinessCards (CardID NUMBER PRIMARY KEY, " +
                            "Name VARCHAR2(50) NOT NULL, " +
                            "PhoneNumber VARCHAR2(20) NOT NULL, " +
                            "Email VARCHAR2(50), " +
                            "Company VARCHAR2(100), " +
                            "Address VARCHAR2(200))";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "CREATE SEQUENCE SEQ_BUSINESSCARDS " +
                            "START WITH 1 " +
                             "INCREMENT BY 1 " +
                             "MAXVALUE 9999 " +
                             "MINVALUE 0 " +
                             "NOCYCLE " +
                             "NOCACHE";
                        cmd.ExecuteNonQuery();
                        break;
                    case 1://명함 추가
                        Console.WriteLine("추가할 명함을 입력하세요 : ");
                        
                        string name0, phone0, email0, company0, address0;
                        
                        
                        Console.Write("이름: ");
                        name0 = Console.ReadLine();
                        
                        Console.Write("\n전화번호: ");
                        phone0 = Console.ReadLine();
                        
                        Console.Write("\n이메일: ");
                        email0 = Console.ReadLine();
                        
                        Console.Write("\n회사: ");
                        company0 = Console.ReadLine();
                        
                        Console.Write("\n주소: ");
                        address0 = Console.ReadLine();

                        cmd.CommandText = "INSERT INTO BUSINESSCARDS (CARDID, NAME, PHONENUMBER, EMAIL, COMPANY, ADDRESS) " +
                                         $"VALUES ({seq}, '{name0}', '{phone0}', '{email0}', '{company0}', '{address0}')";
                        cmd.ExecuteNonQuery();
                        
                        break;
                    case 2://명함 목록 보기
                        cmd.CommandText = "SELECT * FROM BUSINESSCARDS ";
                        cmd.ExecuteNonQuery();
                        OracleDataReader rdr = cmd.ExecuteReader();
                        
                        while (rdr.Read())
                        {
                            int id = int.Parse(rdr["CARDID"].ToString());
                            string name = rdr["NAME"] as string;
                            string hp = rdr["PHONENUMBER"] as string;
                            string email = rdr["EMAIL"] as string;
                            string company = rdr["COMPANY"] as string;
                            string add = rdr["ADDRESS"] as string;

                            Console.WriteLine($"{id} : {name} : {hp} : {email} : {company} : {add}");
                        }
                        break;
                    case 3://명함 수정
                        OracleDataReader rdr1 = cmd.ExecuteReader();

                        while (rdr1.Read())
                        {
                            int id = int.Parse(rdr1["CARDID"].ToString());
                            string name = rdr1["NAME"] as string;
                            string hp = rdr1["PHONENUMBER"] as string;
                            string email = rdr1["EMAIL"] as string;
                            string company = rdr1["COMPANY"] as string;
                            string add = rdr1["ADDRESS"] as string;
                            Console.WriteLine($"{id} : {name} : {hp} : {email} : {company} : {add}");
                        }
                        Console.Write("수정할 명함의 번호를 입력하세요 : ");
                        int id_1 = int.Parse(Console.ReadLine());

                        Console.Write($"\n수정할 항목 (1:이름, 2:전화번호, 3:이메일, 4:회사, 5:주소):");
                        string name_1;
                        switch (int.Parse(Console.ReadLine()))
                        {

                            case 1:
                                Console.Write("새로운 값:");
                                name_1 = Console.ReadLine();
                                cmd.CommandText = "UPDATE BUSINESSCARDS " +
                                                  "SET NAME =  " + $"'{name_1}'" +
                                                  "WHERE CARDID = " + $"{id_1}";
                                cmd.ExecuteNonQuery();
                                break;
                            case 2:
                                Console.Write("새로운 값:");
                                name_1 = Console.ReadLine();
                                cmd.CommandText = "UPDATE BUSINESSCARDS " +
                                                  "SET PHONENUMBER =  " + $"'{name_1}'" +
                                                  "WHERE CARDID = " + $"{id_1}";
                                cmd.ExecuteNonQuery();
                                break;
                            case 3:
                                Console.Write("새로운 값:");
                                name_1 = Console.ReadLine();
                                cmd.CommandText = "UPDATE BUSINESSCARDS " +
                                                  "SET EMAIL =  " + $"'{name_1}'" +
                                                  "WHERE CARDID = " + $"{id_1}";
                                cmd.ExecuteNonQuery();
                                break;
                            case 4:
                                Console.Write("새로운 값:");
                                name_1 = Console.ReadLine();
                                cmd.CommandText = "UPDATE BUSINESSCARDS " +
                                                  "SET COMPANY =  " + $"'{name_1}'" +
                                                  "WHERE CARDID = " + $"{id_1}";
                                cmd.ExecuteNonQuery();
                                break;
                            case 5:
                                Console.Write("새로운 값:");
                                name_1 = Console.ReadLine();
                                cmd.CommandText = "UPDATE BUSINESSCARDS " +
                                                  "SET ADDRESS =  " + $"'{name_1}'" +
                                                  "WHERE CARDID = " + $"{id_1}";
                                cmd.ExecuteNonQuery();
                                break;
                            default:
                                break;
                        }
                        break;
                    case 4://명함삭제
                        OracleDataReader rdr2 = cmd.ExecuteReader();

                        while (rdr2.Read())
                        {
                            int id = int.Parse(rdr2["CARDID"].ToString());
                            string name = rdr2["NAME"] as string;
                            string hp = rdr2["PHONENUMBER"] as string;
                            string email = rdr2["EMAIL"] as string;
                            string company = rdr2["COMPANY"] as string;
                            string add = rdr2["ADDRESS"] as string;
                            Console.WriteLine($"{id} : {name} : {hp} : {email} : {company} : {add}");
                        }
                        Console.Write($"\n삭제할 명함의 번호를 입력하세요");
                        int id_2 = int.Parse(Console.ReadLine());
                        Console.WriteLine("정말 삭제하시겠습니까?");
                        string yes = Console.ReadLine();
                        if (yes == "y")
                        {
                            cmd.CommandText = "DELETE FROM BUSINESSCARDS " +
                                              "WHERE CARDID = " + $"{id_2}";
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("삭제완료.");
                            num = id_2;
                        }
                        else if (yes == "n")
                        {
                            Console.WriteLine("취소했습니다.");
                        }
                        break;
                    case 5://명함검색
                        Console.WriteLine("검색할 이름을 입력하세요 : ");
                        string str = Console.ReadLine();
                        //cmd.CommandText = "SELECT * FROM BUSINESSCARDS " +
                        //                  $"WHERE NAME LIKE %'{str}'%";
                        //cmd.ExecuteNonQuery();
                        OracleDataReader rdr3 = cmd.ExecuteReader();

                        while (rdr3.Read())
                        {
                            int id = int.Parse(rdr3["CARDID"].ToString());
                            string name = rdr3["NAME"] as string;
                            string hp = rdr3["PHONENUMBER"] as string;
                            string email = rdr3["EMAIL"] as string;
                            string company = rdr3["COMPANY"] as string;
                            string add = rdr3["ADDRESS"] as string;
                            if(str == name)
                            {
                                Console.WriteLine($"{id} : {name} : {hp} : {email} : {company} : {add}");
                            }
                        }

                        break;
                    case 6:
                        end = 1;
                        break;
                    default:
                        break;
                }
                if (end == 1)
                    break;
            }//end of last while

            //명함 추가 로직
            //cmd.CommandText = ""
            //3.3 쿼리 실행하기
            //cmd.ExecuteNonQuery();

            //4. 리소스 반환 및 종료
            conn.Close();
        }
    }
}
