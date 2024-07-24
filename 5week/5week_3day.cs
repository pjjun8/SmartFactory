
DECLARE
    V_NUM NUMBER := 1;
BEGIN
    WHILE V_NUM <= 10 LOOP
        IF MOD(V_NUM, 3) = 0 THEN
        DBMS_OUTPUT.PUT_LINE(V_NUM);
        END IF;
        V_NUM := V_NUM + 1;
    END LOOP;
END;

BEGIN
    FOR v_num IN REVERSE 0..4 LOOP
        DBMS_OUTPUT.PUT_LINE(v_num);
    END LOOP;
END;

--16-21 FOR LOOP 안에 CONTINUE
BEGIN
    FOR i IN 0..20 LOOP
        CONTINUE WHEN MOD(i, 2) =1;
        DBMS_OUTPUT.PUT_LINE('현재 i의 값 : ' || i);
    END LOOP;
END;

--17-1 레코드를 정의해서 사용하기
DECLARE
    TYPE REC_DEPT IS RECORD(
        deptno  NUMBER(2) NOT NULL := 99,
        dname   DEPT.DNAME%TYPE,
        loc     DEPT.LOC%TYPE
    );
    dept_rec REC_DEPT; -- 객체선언
BEGIN
    dept_rec.deptno := 99;
    dept_rec.dname := 'DATABASE';
    dept_rec.loc := 'SEOUL';
    DBMS_OUTPUT.PUT_LINE(dept_rec.deptno);
    DBMS_OUTPUT.PUT_LINE(dept_rec.dname);
    DBMS_OUTPUT.PUT_LINE(dept_rec.loc);
END;

--17-2 DEPT_RECORD
CREATE TABLE DEPT_RECORD
 AS SELECT * FROM DEPT;
 
SELECT * FROM DEPT_RECORD;

--Q 레코드를 이용해서 데이터를 삽입하세요. DEPT_RECORD 테이블에 아래 값을 INSERT하세요.
DECLARE
    TYPE DEPTREC IS RECORD(
        deptno  NUMBER(2) NOT NULL := 0,
        dname   DEPT.DNAME%TYPE,
        loc     DEPT.LOC%TYPE
    );
    deptrecord DEPTREC;
BEGIN
    deptrecord.deptno := 50;
    deptrecord.dname := 'HOME';
    deptrecord.loc := 'BUSAN';
    INSERT INTO DEPT_RECORD
    VALUES deptrecord;
END;

--사용자 사전
SELECT COUNT(*) FROM DICT;
SELECT * FROM DICTIONARY;

--사용자 사전 (USER_%)
SELECT TABLE_NAME FROM USER_TABLES
ORDER BY TABLE_NAME;

--데이터 사전 ALL_%
SELECT * FROM ALL_TABLES;

--DBA_%
SELECT * FROM DBA_TABLES;
SELECT * FROM DBA_USERS;

--인덱스 확인
SELECT * FROM USER_INDEXES;
SELECT * FROM USER_IND_COLUMNS;

--인덱스 만들기 -> 검색 속도 증가를 위해 사용
CREATE INDEX IDX_EMP_SAL ON EMP(SAL);
SELECT SAL FROM EMP;

--INDEX DROP
DROP INDEX IDX_EMP_SAL;

--VIEW
SELECT * FROM VW_EMP20;
GRANT CREATE VIEW TO SCOTT;
CREATE VIEW VW_EMP20
    AS (SELECT EMPNO, ENAME, JOB, DEPTNO
        FROM EMP
        WHERE DEPTNO = 20);
        
--동의어 (SYNONYM)
GRANT CREATE SYNONYM TO SCOTT;

GRANT CREATE PUBLIC SYNONYM TO SCOTT;

--동의어
CREATE SYNONYM E FOR EMP;

SELECT * FROM E;
DROP SYNONYM E;
--18-1 단일행 데이터 저장하기
DECLARE
    V_DEPT_ROW DEPT%ROWTYPE;
BEGIN
    SELECT DEPTNO, DNAME, LOC INTO V_DEPT_ROW FROM DEPT
    WHERE DEPTNO = 40;
    
    DBMS_OUTPUT.PUT_LINE(V_DEPT_ROW.DEPTNO);
    DBMS_OUTPUT.PUT_LINE(V_DEPT_ROW.DNAME);
    DBMS_OUTPUT.PUT_LINE(V_DEPT_ROW.LOC);
END;

--18-2 단일행 데이터 저장하는 커서 사용
DECLARE
    V_DEPT_ROW DEPT%ROWTYPE;
    --명시적 커서 선언
    CURSOR c1 IS
        SELECT DEPTNO, DNAME, LOC FROM DEPT WHERE DEPTNO = 40;
BEGIN
    --커서사용? 커서열기 OPEN
    OPEN c1;
    --데이터 가공
    FETCH c1 INTO V_DEPT_ROW;
    
    DBMS_OUTPUT.PUT_LINE(V_DEPT_ROW.DEPTNO);
    DBMS_OUTPUT.PUT_LINE(V_DEPT_ROW.DNAME);
    DBMS_OUTPUT.PUT_LINE(V_DEPT_ROW.LOC);
    --커서닫기 리소스 반환
    CLOSE c1;
END;

--18-3
DECLARE
    V_DEPT_ROW DEPT%ROWTYPE;
    --명시적 커서 선언
    CURSOR c1 IS
        SELECT DEPTNO, DNAME, LOC FROM DEPT;
BEGIN
    --커서사용? 커서열기 OPEN
    OPEN c1;
    LOOP
        --데이터 가공
        FETCH c1 INTO V_DEPT_ROW;
        --커서의 모든 행을 읽기위해 속성 지정
        EXIT WHEN c1%NOTFOUND;
        DBMS_OUTPUT.PUT_LINE('부서번호 : ' || V_DEPT_ROW.DEPTNO ||
                             '부서이름 : ' || V_DEPT_ROW.DNAME ||
                             '부서위치 : ' || V_DEPT_ROW.LOC);
    END LOOP;
    --커서닫기 리소스 반환
    CLOSE c1;
END;
/
--18-8
DECLARE
    v_wrong NUMBER;
BEGIN
    SELECT DNAME INTO v_wrong
        FROM DEPT
    WHERE DEPTNO = 10;
EXCEPTION
    WHEN VALUE_ERROR THEN
        DBMS_OUTPUT.PUT_LINE('예외 처리 : 수치 또는 값 오류 발생');
END;
/

--19-1 저장 프로시저
CREATE OR REPLACE PROCEDURE pro_noparam
IS
    V_EMPNO NUMBER(4) := 7788;
    V_ENAME VARCHAR2(10);
BEGIN
    V_ENAME := 'SCOTT';
    DBMS_OUTPUT.PUT_LINE(V_EMPNO);
    DBMS_OUTPUT.PUT_LINE(V_ENAME);
END;
/
EXECUTE pro_noparam;

--익명 프로시저 실행
BEGIN
    pro_noparam;
END;
/

--19-4 USER SOURCE 프로시저 확인
SELECT * FROM USER_SOURCE
WHERE NAME = 'PRO_NOPARAM';

DROP PROCEDURE pro_noparam;

--19-7 프로시저에 파라미터 지정하기
CREATE OR REPLACE PROCEDURE pro_param_in
(
    param1 IN NUMBER,
    param2 NUMBER,
    param3 NUMBER :=3,
    param4 NUMBER DEFAULT 4
)
IS
BEGIN
    DBMS_OUTPUT.PUT_LINE(param1);
    DBMS_OUTPUT.PUT_LINE(param2);
    DBMS_OUTPUT.PUT_LINE(param3);
    DBMS_OUTPUT.PUT_LINE(param4);
END;
/
BEGIN
    pro_param_in(1,2);
END;
/

--19-12
CREATE OR REPLACE PROCEDURE pro_param_out
(
    in_empno IN EMP.EMPNO%TYPE,
    out_ename OUT EMP.ENAME%TYPE,
    out_sal OUT EMP.SAL%TYPE
)
IS
BEGIN
    SELECT ENAME, SAL INTO out_ename, out_sal FROM EMP
    WHERE EMPNO = in_empno;
END pro_param_out;
/

DECLARE
    v_ename EMP.ENAME%TYPE;
    v_sal EMP.SAL%TYPE;
BEGIN
    pro_param_out(7499, v_ename, v_sal);
    DBMS_OUTPUT.PUT_LINE('ENAME : ' || v_ename);
    DBMS_OUTPUT.PUT_LINE('SAL : ' || v_sal);
END;
/
SELECT * FROM EMP;

--19-16 프로시저 오류 정보
CREATE OR REPLACE PROCEDURE pro_err
IS
    err_no  NUMBER;
BEGIN
    err_no = 100;
    DBMS_OUTPUT.PUT_LINE(err_no);
END pro_err;
/
SHOW ERRORS;

--19-18
SELECT * FROM USER_ERRORS WHERE NAME = 'PRO_ERR';

--19-19 함수
CREATE OR REPLACE FUNCTION func_aftertax(
    sal IN NUMBER
)
RETURN NUMBER
IS
    tax NUMBER := 0.05;
BEGIN
    RETURN(ROUND(sal - (sal * tax)));
END func_aftertax;
/

--19-20 함수 사용
DECLARE
    aftertax NUMBER;
BEGIN
    aftertax := func_aftertax(3000);
    DBMS_OUTPUT.PUT_LINE(aftertax);
END;
/

SELECT func_aftertax(3000) FROM DUAL;
SELECT EMPNO, ENAME, SAL, func_aftertax(SAL) FROM EMP;

--19-24 PACKAGE
CREATE OR REPLACE PACKAGE pkg_example
IS
    spec_no NUMBER := 10;
    FUNCTION func_aftertax(sal NUMBER) RETURN NUMBER;
    PROCEDURE pro_emp(in_empno IN EMP.EMPNO%TYPE);
    PROCEDURE pro_dept(in_deptno IN DEPT.DEPTNO%TYPE);
END;
/
--사용
SELECT TEXT FROM USER_SOURCE WHERE TYPE = 'PACKGE' AND NAME = 'PKG_EXAMPLE';

--Trigger
CREATE TABLE EMP_TRG AS SELECT * FROM EMP;

SELECT * FROM EMP_TRG;

--19-32 주말 작업 불가 트리거
CREATE OR REPLACE TRIGGER trg_emp_nodml_weekend
BEFORE
INSERT OR UPDATE OR DELETE ON EMP_TRG
BEGIN
    IF TO_CHAR(sysdate, 'DY') IN ('수', '일') THEN
        IF INSERTING THEN
            raise_application_error(-2000, '주말 사원정보 추가 불가');
        ELSIF UPDATING THEN
            raise_application_error(-2001, '주말 사원정보 수정 불가');
        ELSIF DELETING THEN
            raise_application_error(-2002, '주말 사원정보 삭제 불가');
        ELSE
            raise_application_error(-2003, '주말 사원정보 변경 불가');
        END IF;
    END IF;
END;
/
SELECT * FROM EMP_TRG WHERE ENAME = 'KING';
--평일 날짜 갱신
UPDATE emp_trg SET sal = 5000
WHERE empno = 7839;
--주말 날짜 갱신

--19-35 로그 테이블
CREATE TABLE EMP_TRG_LOG(
    TABLENAME   VARCHAR2(10),
    DML_TYPE    VARCHAR2(10),
    EMPNO       NUMBER(4),
    USER_NAME   VARCHAR2(30),
    CHANGE_DATE DATE
);

--19-36
CREATE OR REPLACE TRIGGER trg_emp_log
AFTER
INSERT OR UPDATE OR DELETE ON EMP_TRG
FOR EACH ROW
BEGIN
    IF INSERTING THEN
        INSERT INTO emp_trg_log
        VALUES ('EMP_TRG', 'INSERT', :new.empno, SYS_CONTEXT('USERENV', 'SESSION_USER'), sysdate);
    ELSIF UPDATING THEN
        INSERT INTO emp_trg_log
        VALUES ('EMP_TRG', 'UPDATE', :old.empno, SYS_CONTEXT('USERENV', 'SESSION_USER'), sysdate);
    ELSIF DELETING THEN
        INSERT INTO emp_trg_log
        VALUES ('EMP_TRG', 'DELETE', :old.empno, SYS_CONTEXT('USERENV', 'SESSION_USER'), sysdate);
    END IF;
END;
/
--19-37 DATA INSERT
INSERT INTO EMP_TRG
 VALUES(9999, 'TestEmp', 'CLERK', 7788, TO_DATE('2018-03-03', 'YYYY-MM-DD'), 1200, null, 20);

SELECT * FROM EMP_TRG;
SELECT * FROM EMP_TRG_LOG;
COMMIT;
