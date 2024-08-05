using Oracle.ManagedDataAccess.Client;

namespace ConsoleApp24
{
    internal class Program
    {
        static void Menu()
        {
            Console.WriteLine("\n=============================");
            Console.WriteLine("\n0. 라면 테이블 초기화");
            Console.WriteLine("1. 라면제품 제조, 생성");
            Console.WriteLine("2. 라면제품 삭제");
            Console.WriteLine("3. 라면제품 검색");
            Console.WriteLine("4. 라면제품 수정");
            Console.WriteLine("5. 커맨드 종료");
            Console.WriteLine("\n=============================");
        }
        static void CreateTable(OracleCommand cmd)
        {
            //테이블 생성문, 삭제문
            cmd.CommandText = "DROP TABLE NOODLEFACTORY";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DROP TABLE ACENOODLE";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "CREATE TABLE NOODLEFACTORY(" +
                              "ID       NUMBER NOT NULL PRIMARY KEY, " +
                              "NAME     VARCHAR2(20) NOT NULL, " +
                              "PRICE    VARCHAR2(20), " +
                              "DOM      VARCHAR2(20), " +
                              "CAL      VARCHAR2(20))";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "CREATE TABLE ACENOODLE(" +
                              "ID       NUMBER NOT NULL PRIMARY KEY, " +
                              "NAME     VARCHAR2(20) NOT NULL, " +
                              "PRICE    VARCHAR2(20), " +
                              "DOM      VARCHAR2(20), " +
                              "CAL      VARCHAR2(20))";
            cmd.ExecuteNonQuery();
        }
        static void InsertTable(OracleCommand cmd, int id)
        {
            //라면 제조, 생성문
            id = 1; string name = "", price = "", dom = "", cal = "";

            cmd.CommandText = "SELECT * FROM NOODLEFACTORY ORDER BY ID";
            cmd.ExecuteNonQuery();

            OracleDataReader rdr3 = cmd.ExecuteReader();
            while (rdr3.Read())
            {
                int rid = int.Parse(rdr3["ID"].ToString());
                if (rid == id)
                    id++;
            }
            Console.Write("\n라면 이름 : "); name = Console.ReadLine();
            Console.Write("\n라면 가격 : "); price = Console.ReadLine();
            Console.Write("\n라면 제조일 : "); dom = Console.ReadLine();
            Console.Write("\n라면 칼로리 : "); cal = Console.ReadLine();
            cmd.CommandText = "INSERT INTO NOODLEFACTORY(ID, NAME, PRICE, DOM, CAL) " +
                              $"VALUES ({id}, '{name}', '{price}', '{dom}', '{cal}')";
            cmd.ExecuteNonQuery();
            id = 1;
            Console.Write("제품 생성 완료");
        }
        static void DeleteTableValue(OracleCommand cmd)
        {
            //라면 제품 삭제
            cmd.CommandText = "SELECT * FROM NOODLEFACTORY ORDER BY ID";
            cmd.ExecuteNonQuery();

            OracleDataReader rdr2 = cmd.ExecuteReader();
            Console.WriteLine("번호 |  이름   | 가격 |   제조일   |  칼로리");
            while (rdr2.Read())
            {
                int rid = int.Parse(rdr2["ID"].ToString());
                string rname = rdr2["NAME"] as string;
                string rprice = rdr2["PRICE"] as string;
                string rdom = rdr2["DOM"] as string;
                string rcal = rdr2["CAL"] as string;
                Console.WriteLine($"{rid}    | {rname}  | {rprice} | {rdom} | {rcal}");
            }
            Console.Write("삭제할 제품 번호 입력 : "); int delete = int.Parse(Console.ReadLine());
            Console.WriteLine();

            cmd.CommandText = $"DELETE FROM NOODLEFACTORY WHERE ID = {delete}";
            cmd.ExecuteNonQuery();
            Console.WriteLine("삭제 완료");
        }
        static void TableSearch(OracleCommand cmd)
        {
            //제품검색
            cmd.CommandText = "SELECT * FROM NOODLEFACTORY ORDER BY ID";
            cmd.ExecuteNonQuery();

            OracleDataReader rdr = cmd.ExecuteReader();
            Console.WriteLine();
            Console.WriteLine("번호 |  이름   | 가격 |   제조일   |  칼로리");
            while (rdr.Read())
            {
                int rid = int.Parse(rdr["ID"].ToString());
                string rname = rdr["NAME"] as string;
                string rprice = rdr["PRICE"] as string;
                string rdom = rdr["DOM"] as string;
                string rcal = rdr["CAL"] as string;
                Console.WriteLine($"{rid}    | {rname}  | {rprice} | {rdom} | {rcal}");
            }
        }
        static void UpdateTable(OracleCommand cmd)
        {
            //라면제품 수정
            cmd.CommandText = "SELECT * FROM NOODLEFACTORY ORDER BY ID";
            cmd.ExecuteNonQuery();
            OracleDataReader rdr1 = cmd.ExecuteReader();
            Console.WriteLine();
            Console.WriteLine("번호 |  이름   | 가격 |   제조일   |  칼로리");
            while (rdr1.Read())
            {
                int rid = int.Parse(rdr1["ID"].ToString());
                string rname = rdr1["NAME"] as string;
                string rprice = rdr1["PRICE"] as string;
                string rdom = rdr1["DOM"] as string;
                string rcal = rdr1["CAL"] as string;
                Console.WriteLine($"{rid}    | {rname}  | {rprice} | {rdom} | {rcal}");
            }
            Console.Write("수정할 제품 번호입력 : ");
            int upid = int.Parse(Console.ReadLine());

            Console.Write("\n\n수정할 항목 1: 제품명, 2: 제품가격, 3: 제품 제조일, 4: 제품 칼로리 : ");
            int upnum = int.Parse(Console.ReadLine());
            Console.WriteLine();
            string update;
            switch (upnum)
            {
                case 1:
                    Console.Write("새로운 제품명 : ");
                    update = Console.ReadLine();
                    cmd.CommandText = "UPDATE NOODLEFACTORY " +
                                     $"SET NAME = '{update}'" +
                                     $"WHERE ID = {upid}";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("\n수정완료");
                    break;
                case 2:
                    Console.Write("새로운 제품가격 : ");
                    update = Console.ReadLine();
                    cmd.CommandText = "UPDATE NOODLEFACTORY " +
                                     $"SET PRICE = '{update}'" +
                                     $"WHERE ID = {upid}";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("\n수정완료");
                    break;
                case 3:
                    Console.Write("새로운 제품 제조일 : ");
                    update = Console.ReadLine();
                    cmd.CommandText = "UPDATE NOODLEFACTORY " +
                                     $"SET DOM = '{update}'" +
                                     $"WHERE ID = {upid}";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("\n수정완료");
                    break;
                case 4:
                    Console.Write("새로운 제품칼로리 : ");
                    update = Console.ReadLine();
                    cmd.CommandText = "UPDATE NOODLEFACTORY " +
                                     $"SET CAL = '{update}'" +
                                     $"WHERE ID = {upid}";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("\n수정완료");
                    break;
                default:
                    Console.WriteLine("알수없는 오류");
                    break;
            }//end of update switch
        }
        static void AceNoodle(OracleCommand cmd)
        {//미완성 코드
            cmd.CommandText = $"SELECT * FROM NOODLEFACTORY WHERE PRICE = MAX(PRICE)";
            cmd.ExecuteNonQuery();
            OracleDataReader odr = cmd.ExecuteReader();
            Console.WriteLine("최고 가격 라면");
            while(odr.Read())
            {
                cmd.CommandText = "INSERT INTO ACENOODLE (ID, NAME, PRICE, DOM, CAL) " +
                                 $"VALUES ({int.Parse(odr["ID"].ToString())}, '{odr["NAME"] as string}', '{odr["PRICE"] as string}', '{odr["DOM"] as string}', '{odr["CAL"] as string}')";
                cmd.ExecuteNonQuery();
            }
            
        }
        static void Main(string[] args)
        {
            string strConn = "Data Source=(DESCRIPTION=" +
                "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
                "(HOST=localhost)(PORT=1521)))" +
                "(CONNECT_DATA=(SERVER=DEDICATED)" +
                "(SERVICE_NAME=xe)));" +
                "User Id=scott;Password=tiger;";

            //1.연결 객체 만들기 - Client
            OracleConnection conn = new OracleConnection(strConn);

            //2.데이터베이스 접속을 위한 연결
            conn.Open();

            //3.서버와 함께 신나게 놀기

            //명령객체 생성
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            
            int id = 0;
            bool end = false;

            while (end == false) {
                Menu();
                Console.Write("메뉴 선택 : "); int num = int.Parse(Console.ReadLine());
                switch (num)
                {
                    case 0:
                        //테이블 생성문, 삭제문
                        CreateTable(cmd);
                        break;
                    case 1:
                        //라면 제조, 생성문
                        InsertTable(cmd, id);
                        break;
                    case 2:
                        //라면 제품 삭제
                        DeleteTableValue(cmd);
                        break;
                    case 3:
                        //제품검색
                        TableSearch(cmd);
                        break;
                    case 4:
                        //제품수정
                        UpdateTable(cmd);
                        break;
                    case 5:
                        //커맨드 종료
                        end = true;
                        break;
                    case 6:
                        //최고 매출 라면
                        AceNoodle(cmd); //미완성 코드
                        break;
                    default:
                        Console.WriteLine("알수없는 오류");
                        break;
                }//end of main switch
            }//end of main while
            //4. 리소스 반환 및 종료
            conn.Close();
        }
    }
}

