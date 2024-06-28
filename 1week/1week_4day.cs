 char ch = 'A';
 Console.WriteLine(ch);

 char[] arr = new char[3];
 arr[0] = 'a';
 arr[1] = 'b';
 arr[2] = 'c';

 Console.WriteLine($"{arr[0]}{arr[1]}{arr[2]}");

 for (int i = 0; i < 3; i++)
 { 
     Console.Write(arr[i]);
 }
---------------------------------------------------------------------------
           // 크기가 5인 정수형 배열 arrint를 선언하고
           // 값은 10 20 30 40 50 을 입력해 보자.

            int[] arrint = new int[5];

            for (int i = 0; i < arrint.Length; i++)
            {
                arrint[i] = (i + 1) * 10;    
                Console.WriteLine(arrint[i]);
            }
----------------------------------------------------------------------------
  // 크기가 3인 문자 배열 arr를 만드세요.
  // 값은 'Z', 'Y', 'X'
  // 화면에 요소값을 index 순서대로 출력해 보세요.

  char[] arr = new char[26];
  char ch = 'Z';

  for (int i = 0; i < arr.Length; i++)
  {
      arr[i] = ch--;
      Console.WriteLine(arr[i]);
  }
----------------------------------------------------------------------------
