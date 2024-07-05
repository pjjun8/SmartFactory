namespace RandomApp01
{
    internal class Program
    {   //국어, 영어, 수학 성적을 Rando을 사용하여 받고 평균만 출력해 보세요!
        //
        static void Main(string[] args)
        {
            Random random = new Random();
            int[] score = new int[3];
            int total = 0;
            double avg = 0;
            for (int i = 0; i < 3; i++)
            {
                score[i] = random.Next(0, 101);
                Console.WriteLine(score[i]);
                total += score[i];
            }
            avg = (double)total / score.Length;
            Console.WriteLine(avg);
        }
    }
}

