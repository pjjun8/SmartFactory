/*
javascript 자료형!!

1. **Number 숫자**
2. **String 문자열**
3. **Boolean 논리**
4. **Undefined 값 없음**
5. **Null**
6. **Object**
*/
=========================================================
<!DOCTYPE html>
<head>

</head>
<body>
    <script>
        window.onload = function(){

            var myList = '';

            myList += '<ul>';
                myList += '<li>Hello</li>';
                myList += '<li>JavaScript</li>';
            myList += '</ul>';

            document.body.innerHTML = myList;
        };
    </script>
</body>
</html>
=========================================================
<!DOCTYPE html>
<head>

</head>
<body>
    <script>
        var array1 = [273, 32, 103, 57, 52];
        var array2 = [273, 'Hello', true, function(){},[273,103]];
        var array3 = new array(10, 20, 30);
        alert(array2);
        alert(array2[1]);
        //alert(array3[2]);
        //document.body.innerHTML = 
    </script>
</body>
</html>
=========================================================
<!DOCTYPE html>
<head>

</head>
<body>
    <script>
        var func1 = function(){
            var output = prompt('숫자를 입력해 주세요', '숫자');
            document.body.innerHTML = (output);
        };
        func1();
    </script>
</body>
</html>
==========================================================
<!DOCTYPE html>
<html>
<head>

</head>
<body>
    <script>
        var product = {
            제품명: '동결건조 김치찌개',
            유형: '냉동식품',
            성분: '간,설,파,마,후,깨,참,김치,돼지고기',
            원산지: '김성태 집'
        };
        
        var output = '';
        for (var key in product) {
            output += 'e ' + key +':'+product[key] +'\n';
        }
        alert(output);
    </script>
    
</body>
</html>
========================================================
<!DOCTYPE html>
<html>
<head>

</head>
<body>
    <script>
        var child = window.open('','', 'width=300, height=200');

        setInterval(function(){
            child.moveBy(10, 10);
        }, 100)
    </script>
    
</body>
</html>
