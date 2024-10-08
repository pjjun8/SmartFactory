01 #!/bin/sh
02 myvar="Hi Woo"
03 echo $myvar
04 echo "$myvar"
05 echo '$myvar'
06 echo \$myvar
07 echo 값 입력 :
08 read myvar
09 echo '$myvar' = $myvar
10 exit 0
============================
01 #!/bin/sh
02 num1=100
03 num2=$num1 + 200
04 echo $num2
05 num3=`expr $num1 +  200`
06 echo $num3
07 num4=`expr  \(  $num1 + 200  \) / 10  \*  2`
08 echo $num4
09 exit 0
===========================
01 #!/bin/sh
02 echo "실행파일 이름은 <$0>이다"
03 echo "첫번째 파라미터는 <$1>이고, 두번째 파라미터는 <$2>다"
04 echo "전체 파라미터는 <$*>다"
05 exit 0

