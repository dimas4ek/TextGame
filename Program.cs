using System;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace TEXT_GAME
{
    class Program
    {

        public static bool isSuperAttack = false;
        public static StreamReader reader;
        public static StreamWriter saver;
        public static StreamReader saveReader;
        public static int whatSave = 0;
        public static bool isDead = false;
        public static Boss current_boss = new Boss();
        public static Player player = new Player();

        static void Main(string[] args)
        {
            Console.Title = "Текстовая игра";
            Saves();
            GameMenu();
        }

        //Проверяет наличие сохранений перед игрой
        public static void Saves()
        {
            saveReader = new StreamReader("C:/Users/dimas/source/Game/saves.txt");
            whatSave = Convert.ToInt32(saveReader.ReadToEnd());
            saveReader.Close();
        }

        //Меню
        #region GameMenu
        public static void GameMenu()
        {
            Console.WriteLine("Выберите действие:\n\t1 - начать игру\n\t2 - продолжить\n\t3 - выход");
            int choice = Convert.ToInt32(Console.ReadLine());

            string name = "";

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Console.Write("Представься: ");
                    name = Console.ReadLine();
                    Console.Clear();
                    player.Name = name;
                    StartGame();
                    break;
                case 2:
                    ContinueGame();
                    break;
                case 3:
                    QuitGame();
                    break;
            }
        }

        //Старт
        public static void StartGame()
        {
            Thread.Sleep(500);
            Texts();
        }
        //Продолжить
        public static void ContinueGame()
        {
            Console.Clear();
            LoadSave();
            RPS();
        }
        //Выйти
        public static void QuitGame()
        {
            Process process = new Process();
        }
        #endregion

        //Основа игры
        public static void Texts()
        {
            #region prelude 

            ////ОСНОВА////
            string text = $"Кагая Убуяшики: Здравствуй, {player.Name}, твоим наставником будет Тенген Узуй, именно он обучит тебя основам";
            Action(text);
            Console.WriteLine();
            text = $"Тенген Узуй: Привет, {player.Name}, я научу тебя основам, но сначала тебе надо одеть соотвутствующюю униформу";
            Action(text);
            Console.WriteLine();

            ////ВЫБОР ФОРМЫ////
            string path = "C:/Users/dimas/source/Game/prelude/form_choice/form_choice.txt";
            reader = new StreamReader(path);
            Action(reader.ReadToEnd());
            int form_choice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            if (form_choice == 1)
            {
                player.Attack += 5;
                Console.WriteLine("\n+ легкая униформа\n(Урон +5)");
                path = "C:/Users/dimas/source/Game/prelude/form_choice/form13.txt";
            }
            else if (form_choice == 3)
            {
                player.Health += 10;
                Console.WriteLine("\n+ тяжелая униформа\n(Защита +10)");
                path = "C:/Users/dimas/source/Game/prelude/form_choice/form13.txt";
            }
            else if (form_choice == 2)
            {
                isSuperAttack = true;
                Console.WriteLine("\n+ блестящая униформа\n+ суператака");
                path = "C:/Users/dimas/source/Game/prelude/form_choice/form2.txt";
            }
            Pause();
            Console.Clear();
            reader = new StreamReader(path);
            Action(reader.ReadToEnd());
            Pause();
            Console.Clear();

            ////ТРЕНИРОВКА НА ВЫБОР////
            path = "C:/Users/dimas/source/Game/prelude/train_choice/train_choice.txt";
            reader = new StreamReader(path);
            Action(reader.ReadToEnd());
            int train_choice = Convert.ToInt32(Console.ReadLine());
            if (train_choice == 1)
            {
                player.Attack += 10;
                player.Health += 20;
                path = "C:/Users/dimas/source/Game/prelude/train_choice/train1.txt";
                reader = new StreamReader(path);
                Action(reader.ReadToEnd());
                Pause();
                Console.Clear();
                ////ОСНОВА////
                text = "Тенген Узуй: Тебе надо отправиться в сторону востока. Там ты найдёшь \"Храм\", в котором сидит первая луна. Если справишься то Ворон подскажет что делать дальше";
                Action(text);
                Pause();
                Console.Clear();
            }
            else
            {
                ////ОСНОВА////
                Console.Clear();
                text = "Тенген Узуй: Тебе надо отправиться в сторону востока. Там ты найдёшь \"Храм\", в котором сидит первая луна. Если справишься то Ворон подскажет что делать дальше";
                Action(text);
                Pause();
                Console.Clear();
            }

            #endregion

            #region act1

            Action($"{player.Name} отправился в сторону востока");
            Pause();
            Console.Clear();

            path = "C:/Users/dimas/source/Game/acts/act1_1.txt";
            reader = new StreamReader(path);
            Action(reader.ReadToEnd());
            Pause();
            Console.Clear();

            Action($"Неизвестный: ЭЙЙЙ ПАРНИШКА ТЫ НЕ РАНЕН?!\n{player.Name}: Со мной всё в порядке, всего лишь ударился головой");
            Action("\n(Истребителем демонов оказался на вид молодой мускулистый парень с голым торсом, на голове у него странная маска кабана)\n(Незнакомец решил представиться)");
            Pause();
            Console.Clear();

            Action("Незнакомец: Я Иноске, а как тебя зовут?" +
                  $"\n{player.Name}: Меня зовут {player.Name}, я отправляюсь в \"Храм\"");
            if (form_choice == 1)
                Action("Иноске: Приятно познакомиться чудак в легкой форме");
            if (form_choice == 2)
                Action("Иноске: Приятно познакомиться чудак в блестящей форме");
            if (form_choice == 3)
                Action("Иноске: Приятно познакомиться чудак в тяжелой форме");
            Action("\nИноске: Ещё встретимся");
            Pause();
            Console.Clear();

            whatSave = 1;
            ActiveBoss();
            LoadSave();
            saveReader = new StreamReader("C:/Users/dimas/source/Game/saves.txt");
            if (!saveReader.ReadLine().Contains("1"))
            {
                saveReader.Close();
                Action("Бой перед твоим первым боссом наполняет тебя решимостью\n\nСохраниться?");
                SAVE();
            }

            RPS();

            #endregion

            #region act2

            path = "C:/Users/dimas/source/Game/acts/act2_1.txt";
            reader = new StreamReader(path);
            int i = 0;
            while (i < 5)
            {
                Action(reader.ReadLine());
                if (i == 2)
                {
                    Pause();
                    Console.Clear();
                }
                i++;
            }
            i = 0;
            Pause();
            Console.Clear();

            whatSave = 2;
            ActiveBoss();
            LoadSave();
            saveReader = new StreamReader("C:/Users/dimas/source/Game/saves.txt");
            if (!saveReader.ReadLine().Contains("2"))
            {
                saveReader.Close();
                Action("Ненависть окутала тебя, что будет дальше?\n\nСохраниться?");
                SAVE();
            }

            RPS();

            #endregion

            #region act3

            path = "C:/Users/dimas/source/Game/acts/act3_1.txt";
            reader = new StreamReader(path);
            while (i < 21)
            {
                if (i == 6 || i == 10)
                {
                    Pause();
                    Console.Clear();
                }
                Action(reader.ReadLine());
                i++;
            }

            whatSave = 3;
            ActiveBoss();
            LoadSave();
            saveReader = new StreamReader("C:/Users/dimas/source/Game/saves.txt");
            if (!saveReader.ReadLine().Contains("3"))
            {
                saveReader.Close();
                Action("Какя-то обезьяна преградила вам дорогу\n\nСохраниться?");
                SAVE();
            }

            RPS();

            #endregion

            #region act4

            path = "C:/Users/dimas/source/Game/acts/act4_1.txt";
            reader = new StreamReader(path);
            i = 0;
            while (i < 10)
            {
                Action(reader.ReadLine());
                if (i == 4)
                {
                    Pause();
                    Console.Clear();
                }
                i++;
            }
            Pause();
            Console.Clear();

            whatSave = 4;
            ActiveBoss();
            LoadSave();
            saveReader = new StreamReader("C:/Users/dimas/source/Game/saves.txt");
            if (!saveReader.ReadLine().Contains("4"))
            {
                saveReader.Close();
                Action("Стоящий перед вами демон оказался старшим братом демона-обезьяны\nВаши руки дрожат\n\nСохраниться?");
                SAVE();
            }

            RPS();

            Console.WriteLine("Перед смертью Старшой рассказал, что Руководитель Лун находится в \"Крепости Программистов\"" +
                              "\nВы отправились туда");

            #endregion

            #region act5

            string line;
            path = "C:/Users/dimas/source/Game/acts/act5_1.txt";
            reader = new StreamReader(path);
            while ((line = reader.ReadLine()) != null)
            {
                Action(line);
            }
            Pause();
            Console.Clear();

            Action2("\n\n\t\t\t\t\tЕВГЕНИЙ СЕРГЕЕВИЧ ПЛОТНИКОВ" +
                  "\n\n\t\t\t\t\t    (РУКОВОДИТЕЛЬ ЛУН)");
            Pause();
            Console.Clear();

            Action("Стоявший перед вами демон? оказался вашим бывшим сенсеем, обучавший вас C#");
            Pause();
            Console.Clear();

            path = "C:/Users/dimas/source/Game/acts/tryapka_choice.txt";
            reader = new StreamReader(path);
            while ((line = reader.ReadLine()) != null)
            {
                Action(line);
            }
            int tryapka_choice = Convert.ToInt32(Console.ReadLine());
            if (tryapka_choice == 1)
            {
                Console.Clear();
                Action("Плотников-Сенсей: Послушный милюзга, давай посмотрим как ты изучал искусства C#");
                Pause();
                Console.Clear();
                saveReader = new StreamReader("C:/Users/dimas/source/Game/saves.txt");
                whatSave = 5;
                ActiveBoss();
                if (!saveReader.ReadLine().Contains("5"))
                {
                    saveReader.Close();
                    Action("Ты решил показать, кто здесь тру C# кодер\n\nСохраниться?");
                    SAVE();
                }
                LoadSave();

                RPS();
            }
            else if (tryapka_choice == 2)
            {
                Console.Clear();
                Action("Плотников-Сенсей (В ЯРОСТИ): АХ ТЫ МЕЛКАЯ СОШКА, КАК ТЫ ПОСМЕЛ ПРИЙТИ НА МОЮ ПАРУ В ГРЯЗНОЙ ОБУВИ" +
                       "\nПлотников-Сенсей (В ЯРОСТИ): Я ВЫТЕРУ ТВОИ ГРЯЗНЫЕ БАШМАКИ О ТВОЕ ЖАЛКОЕ ЛИЦО" +
                       "\n\n(Атака и здоровье демона повышены)");
                Pause();
                Console.Clear();
                whatSave = 6;
                ActiveBoss();
                saveReader = new StreamReader("C:/Users/dimas/source/Game/saves.txt");
                if (!saveReader.ReadLine().Contains("6"))
                {
                    saveReader.Close();
                    Action("Кажется ты влип...\n\nСохраниться?");
                    SAVE();
                }
                LoadSave();

                RPS();
            }

            #endregion
        }


        //Разные методы
        public static void Action(string text)
        {
            int i = 0;
            while (i < text.Length)
            {
                Console.Write(text[i]);
                Thread.Sleep(20);
                i++;
            }
            Console.WriteLine();
            Thread.Sleep(500);
        }
        public static void Action2(string text)
        {
            int i = 0;
            while (i < text.Length)
            {
                Console.Write(text[i]);
                Thread.Sleep(70);
                i++;
            }
            Console.WriteLine();
            Thread.Sleep(500);
        }
        public static void Pause()
        {
            Console.WriteLine("\n---> Продолжить --->");
            Console.ReadKey();
        }

        //Это методы сохранения
        public static void SAVE()
        {
            Console.WriteLine("\nСохранение:\n\t1 - сохраниться\n\t2 - не сохраняться");
            int save_choice = Convert.ToInt32(Console.ReadLine());
            if(save_choice == 1)
            {
                saver = new StreamWriter("C:/Users/dimas/source/Game/saves.txt");
                saver.Write(whatSave);
                saver.Close();

                int i = 0;
                Console.Write("\nСохраняю");
                while (i < 5)
                {
                    Thread.Sleep(400);
                    Console.Write(".");
                    i++;
                }
                Console.WriteLine();

                Console.WriteLine("\nСохранение прошло успешно!");
                Pause();
                Console.Clear();
            }
            if(save_choice == 2)
            {   
                Console.Clear();
            }
        }
        public static void LoadSave()
        {
            if(whatSave == 0)
            {
                Console.WriteLine("Сохранений не найдено\n\nВозвращаю в меню");
                Thread.Sleep(1000);
                Console.Clear();
                GameMenu();
            }
            if(whatSave == 1)
            {
                string path = "C:/Users/dimas/source/Game/acts/act1_2.txt";

                reader = new StreamReader(path);
                int i = 0;
                while (i < 9)
                {
                    if (i < 2)
                        Action(reader.ReadLine());
                    if (i > 3)
                    {
                        Action2(reader.ReadLine());
                    }
                    i++;
                }
                Pause(); 
                Console.Clear();
            }
            else if(whatSave == 2)
            {
                string path = "C:/Users/dimas/source/Game/acts/act2_2.txt";
                reader = new StreamReader(path);
                Action(reader.ReadLine());
                Pause(); 
                Console.Clear();
            }
            else if( whatSave == 3)
            {
                string path = "C:/Users/dimas/source/Game/acts/act3_2.txt";
                reader = new StreamReader(path);
                Action(reader.ReadLine());
                Pause(); 
                Console.Clear();
            }
            else if(whatSave == 4)
            {
                string path = "C:/Users/dimas/source/Game/acts/act4_2.txt";
                reader = new StreamReader(path);
                Action(reader.ReadToEnd());
                Pause();
                Console.Clear();
            }
            else if(whatSave == 5) //5 босс
            {
                Action("Плотников-Сенсей: Ты не сбежишь");
                Thread.Sleep(500);
                Console.Clear();
            }
            else if (whatSave == 6) //5 босс в ярости
            {
                Action("Плотников-Сенсей: Ты не сбежишь");
                Thread.Sleep(500);
                Console.Clear();
            }
        }
        
        //Активный босс
        public static void ActiveBoss()
        {
            switch (whatSave)
            {
                case 1:
                    current_boss.Name = "Кандзи Романдзи";
                    current_boss.Attack = 20;
                    current_boss.Health = 100;
                    break;
                case 2:
                    current_boss.Name = "Демон Ненависти";
                    current_boss.Attack = 25;
                    current_boss.Health = 150;
                    break;
                case 3:
                    current_boss.Name = "Обезьяна-страж";
                    current_boss.Attack = 30;
                    current_boss.Health = 200;
                    break;
                case 4:
                    current_boss.Name = "Старшой";
                    current_boss.Attack = 35;
                    current_boss.Health = 250;
                    break;
                case 5:
                    current_boss.Name = "Плотников-сенсей";
                    current_boss.Attack = 45;
                    current_boss.Health = 350;
                    break;
                case 6:
                    current_boss.Name = "Плотников-сенсей";
                    current_boss.Attack = 60;
                    current_boss.Health = 500;
                    break;
            }
        }

        //Сама игра камень ножницы бумага
        public static void RPS()
        {
            Random random = new Random();

            Console.WriteLine("БИТВА");
            Thread.Sleep(1000);
            Console.Clear();

            while (current_boss.Health >= 0 || player.Health >= 0)
            {
                if (current_boss.Health <= 0)
                {
                    current_boss.Health = 0;
                    break;
                }
                else if (player.Health <= 0)
                {
                    player.Health = 0;
                    break;
                }

                if(!isSuperAttack)
                    Console.Write("1 - Сая (ножны)\n2 - Магия\n3 - Катана\n\nтвой выбор - ");
                else
                    Console.Write("1 - Сая (ножны)\n2 - Магия\n3 - Катана\n4 - суператака\n\nтвой выбор - ");

                int choice = Convert.ToInt32(Console.ReadLine());
                string s_choice = "";
                Console.Clear();
                Console.Write($"{player.Name}: ");
                switch (choice)
                {
                    case 1:
                        s_choice = "Сая (ножны)";
                        Console.Write("Сая (ножны)");
                        break;
                    case 2:
                        s_choice = "Магия";
                        Console.Write("Магия");
                        break;
                    case 3:
                        s_choice = "Катана";
                        Console.Write("Катана");
                        break;
                    case 4:
                        s_choice = "Суператака";
                        break;
                }


                string s_enemy_choice = "";
                int enemy_choice = random.Next(1, 4);
                if (choice != 4)
                {
                    Console.Write($"\n{current_boss.Name}: ");
                    switch (enemy_choice)
                    {
                        case 1:
                            s_enemy_choice = "Сая (ножны)";
                            Console.Write("Сая (ножны)");
                            break;
                        case 2:
                            s_enemy_choice = "Магия";
                            Console.Write("Магия");
                            break;
                        case 3:
                            s_enemy_choice = "катана";
                            Console.Write("Катана");
                            break;
                    }
                }


                if(choice == 4)
                {
                    isSuperAttack = false;
                    Console.Write($"{player.Name} использовал суператаку\n");
                    current_boss.Health -= (current_boss.Health / 2);
                    Console.WriteLine($"\n\t{player.Name}: {player.Health} ХП\n\t{current_boss.Name}: {current_boss.Health} ХП");
                    Pause();
                    Console.Clear();
                }
                if (choice == 1 && enemy_choice == 3)
                {
                    Console.WriteLine("\n\nСая (ножны) > Катана");
                    current_boss.Health -= player.Attack;
                    Console.WriteLine($"\n{player.Name}: нанес {player.Attack} урона\n{current_boss.Name}: -{player.Attack} ХП");
                    Console.WriteLine($"\n\t{player.Name}: {player.Health} ХП\n\t{current_boss.Name}: {current_boss.Health} ХП");
                    Pause();
                    Console.Clear();
                }
                else if (choice < enemy_choice)
                {
                    Console.WriteLine($"\n\n{s_choice} < {s_enemy_choice}");
                    player.Health -= current_boss.Attack;
                    Console.WriteLine($"\n{current_boss.Name}: нанес {current_boss.Attack} урона\n{player.Name}: -{current_boss.Attack} ХП");
                    Console.WriteLine($"\n\t{player.Name}: {player.Health} ХП\n\t{current_boss.Name}: {current_boss.Health} ХП");
                    Pause();
                    Console.Clear();
                }
                if (choice > enemy_choice && choice!=4)
                {
                    Console.WriteLine($"\n\n{s_choice} > {s_enemy_choice}");
                    current_boss.Health -= player.Attack;
                    Console.WriteLine($"\n{player.Name}: нанес {player.Attack} урона\n{current_boss.Name}: -{player.Attack} ХП");
                    Console.WriteLine($"\n\t{player.Name}: {player.Health} ХП\n\t{current_boss.Name}: {current_boss.Health} ХП");
                    Pause();
                    Console.Clear();
                }
                if (choice == enemy_choice)
                {
                    Console.WriteLine("\n\nНичья");
                    Console.WriteLine($"\n\t{player.Name}: {player.Health} ХП\n\t{current_boss.Name}: {current_boss.Health} ХП");
                    Pause();
                    Console.Clear();
                }
            }

            if (current_boss.Health <= 0)
            {
                Console.WriteLine($"{player.Name} победил\n");

                switch (current_boss.Name)
                {
                    case "Кандзи Романдзи":
                        Action("Кандзи Романдзи: Ты пожалеешь о том что стал охо..тником...");
                        Pause();
                        Console.Clear();
                        Console.WriteLine("\n\t(Урон +5)\n\t(Здоровье +10");
                        player.Attack += 5;
                        player.Health += 10;
                        Pause();
                        Console.Clear();
                        break;
                    case "Демон Ненависти":
                        Action("Демон Ненависти: Они обращаются с неудачниками как с мусором, вы ни как не отличаетесь от нас...");
                        Pause();
                        Console.Clear();
                        Console.WriteLine("\n\t(Урон +5)\n\t(Здоровье +10");
                        player.Attack += 5;
                        player.Health += 10;
                        Pause();
                        Console.Clear();
                        break;
                    case "Обезьяна-страж":
                        Action("Обезьяна-страж: Мой брат покончит с начатым, ты не ОСТАНОВИШЬ ЕГО!!!");
                        Pause();
                        Console.Clear();
                        Console.WriteLine("\n\t(Урон +5)\n\t(Здоровье +10");
                        player.Attack += 5;
                        player.Health += 10;
                        Pause();
                        Console.Clear();
                        break;
                    case "Старшой":
                        Action2("Старшой: Мой руководитель... Кха..Кха.. Он прикончит тебя...");
                        Pause();
                        Console.Clear();
                        Console.WriteLine("\n\t(Урон +5)\n\t(Здоровье +10");
                        player.Attack += 5;
                        player.Health += 10;
                        Pause();
                        Console.Clear();
                        break;
                    case "Плотников-сенсей":
                        Action2("Плотников-сенсей: Кха... Чёрт... Ты сдал мой экзамен! Не ожидал от такого слабака как ты!");
                        Pause();
                        Console.Clear();
                        Console.WriteLine("\n\tПОЗДРАВЛЯЮ!\n\tТЫ ПРОШЕЛ ИГРУ!");
                        break;
                }
            }
            else
            {
                Console.WriteLine($"{current_boss.Name} победил\n");

                if (current_boss.Name == "Плотников-сенсей")
                {
                    Action("ХАХАХА... Так и знал что ты неудачник, тебе никогда не одалеть профессионала такого как я!!!");
                    Pause();
                    Console.Clear();
                }

                Console.WriteLine("\nТы погиб\n\t1 - загрузить сохранение\n\t2 - выйти");

                int after_death_choice = Convert.ToInt32(Console.ReadLine());

                if(after_death_choice == 1)
                {
                    if (saveReader.ReadLine().Contains("0"))
                    {
                        Console.WriteLine("Сохранений не найдено\n\nНачать новую игру?");
                    }
                    else
                        LoadSave();
                }
                else if(after_death_choice == 2)
                {
                    Process process = new Process();
                }
            }

        }
    }
}
