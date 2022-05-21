using System;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace TEXT_GAME
{
    class Game
    {
        public static bool isSuperAttack = false;

        public static StreamReader reader;

        public static StreamWriter saver;
        public static StreamReader saveReader;

        public static StreamWriter nameWriter;
        public static StreamReader nameReader;

        public static StreamWriter formWriter;
        public static StreamReader formReader;

        public static StreamWriter trainWriter;
        public static StreamReader trainReader;

        public static StreamWriter statsWriter;
        public static StreamReader statsReader;

        public static int whatSave = 0;
        public static bool isDead = false;
        public static Boss current_boss = new Boss();
        public static Player player = new Player();
        public static string line = "";
        public static int form_choice = 0;
        public static int train_choice = 0;
        public static string current_name;
        public static bool isActChoice = false;

        static void Main(string[] args)
        {
            Console.Title = "Текстовая игра";
            Saves();
            GameMenu();
        }

        //Проверяет сохранение из файла и записывает в переменную
        public static void Saves()
        {
            saveReader = new StreamReader("Game/saves.txt");
            whatSave = Convert.ToInt32(saveReader.ReadToEnd());
            saveReader.Close();
        }

        //Меню
        #region GameMenu
        public static void GameMenu()
        {
            Console.WriteLine("Выберите действие:\n\t1 - начать игру\t\n\t2 - продолжить\n\t3 - перейти к главе\n\t4 - выход");
            int choice = Convert.ToInt32(Console.ReadLine());

            string name = "";

            switch (choice)
            {
                case 1:
                    saver = new StreamWriter("Game/saves.txt");
                    saver.Write(0);
                    saver.Close();

                    Console.Clear();
                    Console.Write("Представься: ");
                    name = Console.ReadLine();
                    Console.Clear();

                    nameWriter = new StreamWriter("Game/name.txt");
                    nameWriter.Write(name);
                    nameWriter.Close();

                    nameReader = new StreamReader("Game/name.txt");
                    current_name = nameReader.ReadToEnd();
                    nameReader.Close();

                    StartGame();
                    break;
                case 2:
                    nameReader = new StreamReader("Game/name.txt");
                    current_name = nameReader.ReadToEnd();
                    nameReader.Close();
                    ContinueGame();
                    break;
                case 3:
                    isActChoice = true;

                    Console.Clear();
                    Console.WriteLine("Вернуться в меню?\n1 - да\n2 - нет");
                    int actToMenu_choice = Convert.ToInt32(Console.ReadLine());
                    if (actToMenu_choice == 1)
                    {
                        Console.Clear();
                        isActChoice = false;
                        GameMenu();
                    }
                    else
                    {
                        saver = new StreamWriter("Game/saves.txt");
                        saver.Write(0);
                        saver.Close();

                        Console.Clear();
                        Console.Write("Представься: ");
                        name = Console.ReadLine();
                        Console.Clear();

                        nameWriter = new StreamWriter("Game/name.txt");
                        nameWriter.Write(name);
                        nameWriter.Close();

                        nameReader = new StreamReader("Game/name.txt");
                        current_name = nameReader.ReadToEnd();
                        nameReader.Close();

                        ToAct();
                    }
                    break;
                case 4:
                    QuitGame();
                    break;
            }
        }

        //Старт
        public static void StartGame()
        {
            Thread.Sleep(500);
            Prelude();
            Act1();
        }
        //Продолжить
        public static void ContinueGame()
        {
            Console.Clear();
            LoadSave();
        }
        //Перейти к главе
        public static void ToAct()
        {
            Console.Clear();

            Console.Write("Выберите главу (1-5): ");
            int act_choice = Convert.ToInt32(Console.ReadLine());
            switch (act_choice)
            {
                case 1:
                    Console.Clear();
                    Act1();
                    break;
                case 2:
                    Console.Clear();
                    Act2();
                    break;
                case 3:
                    Console.Clear();
                    Act3();
                    break;
                case 4:
                    Console.Clear();
                    Act4();
                    break;
                case 5:
                    Console.Clear();
                    Act5();
                    break;
            }
        }
        //Выйти
        public static void QuitGame()
        {
            Process process = new Process();
        }
        #endregion

        //Основа игры
        #region Texts
        public static void Prelude()
        {
            nameReader = new StreamReader("Game/name.txt");

            ////ОСНОВА////
            string text = $"Кагая Убуяшики: Здравствуй, {current_name}, твоим наставником будет Тенген Узуй, именно он обучит тебя основам";
            Action(text);
            Console.WriteLine();
            text = $"Тенген Узуй: Привет, {current_name}, я научу тебя основам, но сначала тебе надо одеть соотвутствующюю униформу";
            Action(text);
            Console.WriteLine();

            ////ВЫБОР ФОРМЫ////
            string path = "Game/prelude/form_choice/form_choice.txt";
            reader = new StreamReader(path);
            Action(reader.ReadToEnd());
            form_choice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            if (form_choice == 1)
            {
                formWriter = new StreamWriter("Game/form.txt");
                formWriter.Write("1");
                formWriter.Close();

                Console.WriteLine("\n+ легкая униформа\n(Урон +5)");
                path = "Game/prelude/form_choice/form13.txt";
            }
            else if (form_choice == 3)
            {
                formWriter = new StreamWriter("Game/form.txt");
                formWriter.Write("3");
                formWriter.Close();

                Console.WriteLine("\n+ тяжелая униформа\n(Защита +10)");
                path = "Game/prelude/form_choice/form13.txt";
            }
            else if (form_choice == 2)
            {
                formWriter = new StreamWriter("Game/form.txt");
                formWriter.Write("2");
                formWriter.Close();

                Console.WriteLine("\n+ блестящая униформа\n+ суператака");
                path = "Game/prelude/form_choice/form2.txt";
            }
            Pause();
            Console.Clear();
            reader = new StreamReader(path);
            Action(reader.ReadToEnd());
            Pause();
            Console.Clear();

            ////ТРЕНИРОВКА НА ВЫБОР////
            path = "Game/prelude/train_choice/train_choice.txt";
            reader = new StreamReader(path);
            Action(reader.ReadToEnd());
            train_choice = Convert.ToInt32(Console.ReadLine());
            if (train_choice == 1)
            {
                trainWriter = new StreamWriter("Game/train.txt");
                trainWriter.Write("1");
                trainWriter.Close();

                path = "Game/prelude/train_choice/train1.txt";
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

            nameReader.Close();
        }
        public static void Act1()
        {
            nameReader = new StreamReader("Game/name.txt");

            Action($"{current_name} отправился в сторону востока");
            Pause();
            Console.Clear();

            string path = "Game/acts/act1_1.txt";
            reader = new StreamReader(path);
            Action(reader.ReadToEnd());
            Pause();
            Console.Clear();
            reader.Close();

            Action($"Неизвестный: ЭЙЙЙ ПАРНИШКА ТЫ НЕ РАНЕН?!\n{current_name}: Со мной всё в порядке, всего лишь ударился головой");
            Action("\n(Истребителем демонов оказался на вид молодой мускулистый парень с голым торсом, на голове у него странная маска кабана)\n(Незнакомец решил представиться)");
            Pause();
            Console.Clear();

            Action("Незнакомец: Я Иноске, а как тебя зовут?" +
                  $"\n{current_name}: Меня зовут {current_name}, я отправляюсь в \"Храм\"");
            if (form_choice == 1)
                Action("Иноске: Приятно познакомиться чудак в легкой форме");
            if (form_choice == 2)
                Action("Иноске: Приятно познакомиться чудак в блестящей форме");
            if (form_choice == 3)
                Action("Иноске: Приятно познакомиться чудак в тяжелой форме");
            Action("\nИноске: Ещё встретимся");
            Pause();
            Console.Clear();

            path = "Game/acts/act1_2.txt";
            reader = new StreamReader(path);
            Action(reader.ReadLine());
            Pause();
            Console.Clear();
            reader.Close();

            whatSave = 1;

            //ActiveBoss();

            saveReader = new StreamReader("Game/saves.txt");
            if (!saveReader.ReadLine().Contains("1"))
            {
                saveReader.Close();
                Action("Бой перед твоим первым боссом наполняет тебя решимостью\n\nСохраниться?");
                SAVE();
            }
            saveReader.Close();
            
            nameReader.Close();

            Console.WriteLine("Пропустить битву с боссом?\n1 - пропустить\n2 - сразиться с боссом");
            int skip_boss = Convert.ToInt32(Console.ReadLine());
            if (skip_boss == 1)
            {
                Console.Clear();
                Act2();
            }
            else
                LoadSave();

            //RPS();
        }
        public static void Act2()
        {
            nameReader = new StreamReader("Game/name.txt");

            string path = "Game/acts/act2.txt";
            reader = new StreamReader(path);
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("METKA"))
                {
                    Pause();
                    Console.Clear();
                    continue;
                }
                Action(line);
            }
            Pause();
            Console.Clear();
            reader.Close();

            whatSave = 2;

            //ActiveBoss();


            saveReader = new StreamReader("Game/saves.txt");
            if (!saveReader.ReadLine().Contains("2"))
            {
                saveReader.Close();
                Action("Ненависть окутала тебя, что будет дальше?\n\nСохраниться?");
                SAVE();
            }
            saveReader.Close();

            nameReader.Close();

            Console.WriteLine("Пропустить битву с боссом?\n1 - пропустить\n2 - сразиться с боссом");
            int skip_boss = Convert.ToInt32(Console.ReadLine());
            if (skip_boss == 1)
            {
                Console.Clear();
                Act3();
            }
            else
                LoadSave();

            //RPS();
        }
        public static void Act3()
        {
            nameReader = new StreamReader("Game/name.txt");

            string path = "Game/acts/act3.txt";
            reader = new StreamReader(path);
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("METKA"))
                {
                    Pause();
                    Console.Clear();
                    continue;
                }
                Action(line);
            }
            Pause();
            Console.Clear();
            reader.Close();

            whatSave = 3;

            //ActiveBoss();


            saveReader = new StreamReader("Game/saves.txt");
            if (!saveReader.ReadLine().Contains("3"))
            {
                saveReader.Close();
                Action("Какая-то обезьяна преградила вам дорогу\n\nСохраниться?");
                SAVE();
            }
            saveReader.Close();

            nameReader.Close();

            Console.WriteLine("Пропустить битву с боссом?\n1 - пропустить\n2 - сразиться с боссом");
            int skip_boss = Convert.ToInt32(Console.ReadLine());
            if (skip_boss == 1)
            {
                Console.Clear();
                Act4();
            }
            else
                LoadSave();

            //RPS();
        }
        public static void Act4()
        {
            nameReader = new StreamReader("Game/name.txt");

            string path = "Game/acts/act4.txt";
            reader = new StreamReader(path);
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("METKA"))
                {
                    Pause();
                    Console.Clear();
                    continue;
                }
                Action(line);
            }
            Pause();
            Console.Clear();
            reader.Close();

            whatSave = 4;

            //ActiveBoss();


            saveReader = new StreamReader("Game/saves.txt");
            if (!saveReader.ReadLine().Contains("4"))
            {
                saveReader.Close();
                Action("Стоящий перед вами демон оказался старшим братом демона-обезьяны\nВаши руки дрожат\n\nСохраниться?");
                SAVE();
            }
            saveReader.Close();

            nameReader.Close();

            Console.WriteLine("Пропустить битву с боссом?\n1 - пропустить\n2 - сразиться с боссом");
            int skip_boss = Convert.ToInt32(Console.ReadLine());
            if (skip_boss == 1)
            {
                Console.Clear();
                Act5();
            }
            else
                LoadSave();

            //RPS();

            //Console.WriteLine("(Перед смертью Старшой рассказал, что Руководитель Лун находится в \"Крепости Программистов\")" +
            //                "\n(Вы отправились туда");
            //Pause();
            //Console.Clear();
        }
        public static void Act5()
        {
            nameReader = new StreamReader("Game/name.txt");

            string path = "Game/acts/act5.txt";
            reader = new StreamReader(path);
            while ((line = reader.ReadLine()) != null)
            {
                Action(line);
            }
            Pause();
            Console.Clear();
            reader.Close();

            Action2("\n\n\t\t\t\t\tЕВГЕНИЙ СЕРГЕЕВИЧ ПЛОТНИКОВ" +
                  "\n\n\t\t\t\t\t    (РУКОВОДИТЕЛЬ ЛУН)");
            Thread.Sleep(1000);
            Console.Clear();

            Action("Стоявший перед вами демон? оказался вашим бывшим сенсеем, обучавший вас C#");
            Pause();
            Console.Clear();

            path = "Game/acts/tryapka_choice.txt";
            reader = new StreamReader(path);
            while ((line = reader.ReadLine()) != null)
            {
                Action(line);
            }
            reader.Close();

            int tryapka_choice = Convert.ToInt32(Console.ReadLine());
            if (tryapka_choice == 1)
            {
                Console.Clear();
                Action("Плотников-Сенсей: Послушный милюзга, давай посмотрим как ты изучал искусства C#");
                Pause();
                Console.Clear();

                whatSave = 5;

                //ActiveBoss();

                saveReader = new StreamReader("Game/saves.txt");
                if (!saveReader.ReadLine().Contains("5"))
                {
                    saveReader.Close();
                    Action("Ты решил показать, кто здесь тру C# кодер\n\nСохраниться?");
                    SAVE();
                }
                saveReader.Close();

                nameReader.Close();

                Console.WriteLine("Пропустить битву с боссом?\n1 - пропустить\n2 - сразиться с боссом");
                int skip_boss = Convert.ToInt32(Console.ReadLine());
                if (skip_boss == 1)
                {
                    Console.Clear();
                    Console.WriteLine("ПОЗДРАВЯЛЮ! ТЫ СБЕЖАЛ ОТ БИТВЫ С ФИНАЛЬНЫМ БОССОМ!");
                }
                else
                    LoadSave();

                //RPS();
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

                //ActiveBoss();

                saveReader = new StreamReader("Game/saves.txt");
                if (!saveReader.ReadLine().Contains("6"))
                {
                    saveReader.Close();
                    Action("Кажется ты влип...\n\nСохраниться?");
                    SAVE();
                }
                saveReader.Close();

                nameReader.Close();

                Console.WriteLine("Пропустить битву с боссом?\n1 - пропустить\n2 - сразиться с боссом");
                int skip_boss = Convert.ToInt32(Console.ReadLine());
                if (skip_boss == 1)
                {
                    Console.Clear();
                    Console.WriteLine("ПОЗДРАВЯЛЮ! ТЫ СБЕЖАЛ ОТ БИТВЫ С ФИНАЛЬНЫМ БОССОМ!");
                }
                else
                    LoadSave();

                //RPS();
            }

        }
        #endregion

        //Текстовые методы
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

        //Это методы сохранения и загрузки из сохранения
        public static void SAVE()
        {
            Console.WriteLine("\nСохранение:\n\t1 - сохраниться\n\t2 - не сохраняться");
            int save_choice = Convert.ToInt32(Console.ReadLine());
            if(save_choice == 1)
            {
                saver = new StreamWriter("Game/saves.txt");
                saver.Write(whatSave);
                saver.Close();

                int i = 0;
                Console.Write("\nСохраняю");
                while (i < 3)
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
            switch (whatSave)
            {
                case 0:
                    Console.WriteLine("Сохранений не найдено\n\nВозвращаю в меню");
                    Thread.Sleep(1000);
                    Console.Clear();
                    GameMenu();
                    break;
                case 1:
                    Action("Кандзи Романдзи: Ты зря пришёл сюда, многих начинающих истребителей я прикончил своими руками");
                    Pause();
                    Console.Clear();
                    ActiveBoss();
                    RPS();
                    break;
                case 2:
                    Action("Демон Ненависти: Я прикончу всех жителей этой деревни, ненавижу этих жителей!");
                    Pause();
                    Console.Clear();
                    ActiveBoss();
                    RPS();
                    break;
                case 3:
                    Action("Обезьяна - страж: Вы пожалеете что решили помешать моему старшему брату");
                    Pause();
                    Console.Clear();
                    ActiveBoss();
                    RPS();
                    break;
                case 4:
                    Action("Старшой: ТЫ УБИЛ МОЕГО БРАТА!\nСтаршой: Я ОТПРАВЛЮ ТЕБЯ ВСЛЕД ЗА НИМ ВМЕСТЕ С ТВОИМ РАНЕННЫМ ДРУЖКОМ!");
                    Pause();
                    Console.Clear();
                    ActiveBoss();
                    RPS();
                    Console.WriteLine("(Перед смертью Старшой рассказал, что Руководитель Лун находится в \"Крепости Программистов\")" +
                                    "\n(Вы отправились туда");
                    Pause();
                    Console.Clear();
                    break;
                case 5:
                    Action("Плотников-Сенсей: Ты не сбежишь");
                    Thread.Sleep(500);
                    Console.Clear();
                    ActiveBoss();
                    RPS();
                    break;
                case 6:
                    Action("Плотников-Сенсей (В ЯРОСТИ): Ты не сбежишь");
                    Thread.Sleep(500);
                    Console.Clear();
                    ActiveBoss();
                    RPS();
                    break;
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

        //Сама игра камень-ножницы-бумага
        public static void RPS()
        {
            Random random = new Random();

            Console.WriteLine("БИТВА");
            Thread.Sleep(1000);
            Console.Clear();

            formReader = new StreamReader("Game/form.txt");
            switch (formReader.ReadLine())
            {
                case "1":
                    player.Attack += 5;
                    break;
                case "2":
                    isSuperAttack = true;
                    break;
                case "3":
                    player.Health += 10;
                    break;
            }
            formReader.Close();

            trainReader = new StreamReader("Game/train.txt");
            if (trainReader.ReadLine() == "1")
            {
                player.Attack += 10;
                player.Health += 20;
            }
            trainReader.Close();

            statsReader = new StreamReader("Game/stats.txt");
            switch (statsReader.ReadLine())
            {
                case "2":
                    player.Attack += 5;
                    player.Health += 10;
                    break;
                case "3":
                    player.Attack += 5;
                    player.Health += 10;
                    break;
                case "4":
                    player.Attack += 5;
                    player.Health += 10;
                    break;
                case "56":
                    player.Attack += 5;
                    player.Health += 10;
                    break;
            }
            statsReader.Close();

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
                Console.Write($"{current_name}: ");
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
                    Console.Write($"{current_name} использовал суператаку\n");
                    current_boss.Health -= (current_boss.Health / 2);
                    Console.WriteLine($"\n\t{current_name}: {player.Health} ХП\n\t{current_boss.Name}: {current_boss.Health} ХП");
                    Pause();
                    Console.Clear();
                }
                if (choice == 1 && enemy_choice == 3)
                {
                    Console.WriteLine("\n\nСая (ножны) > Катана");
                    current_boss.Health -= player.Attack;
                    Console.WriteLine($"\n{current_name}: нанес {player.Attack} урона\n{current_boss.Name}: -{player.Attack} ХП");
                    Console.WriteLine($"\n\t{current_name}: {player.Health} ХП\n\t{current_boss.Name}: {current_boss.Health} ХП");
                    Pause();
                    Console.Clear();
                }
                else if (choice < enemy_choice)
                {
                    Console.WriteLine($"\n\n{s_choice} < {s_enemy_choice}");
                    player.Health -= current_boss.Attack;
                    Console.WriteLine($"\n{current_name}: нанес {current_boss.Attack} урона\n{current_name}: -{current_boss.Attack} ХП");
                    Console.WriteLine($"\n\t{current_name}: {player.Health} ХП\n\t{current_boss.Name}: {current_boss.Health} ХП");
                    Pause();
                    Console.Clear();
                }
                if (choice > enemy_choice && choice!=4)
                {
                    Console.WriteLine($"\n\n{s_choice} > {s_enemy_choice}");
                    current_boss.Health -= player.Attack;
                    Console.WriteLine($"\n{current_name}: нанес {player.Attack} урона\n{current_boss.Name}: -{player.Attack} ХП");
                    Console.WriteLine($"\n\t{current_name}: {player.Health} ХП\n\t{current_boss.Name}: {current_boss.Health} ХП");
                    Pause();
                    Console.Clear();
                }
                if (choice == enemy_choice)
                {
                    Console.WriteLine("\n\nНичья");
                    Console.WriteLine($"\n\t{current_name}: {player.Health} ХП\n\t{current_boss.Name}: {current_boss.Health} ХП");
                    Pause();
                    Console.Clear();
                }
            }
            
            if (current_boss.Health <= 0)
            {
                Console.WriteLine($"{current_name} победил\n");

                if (isActChoice == true)
                {
                    saver = new StreamWriter("Game/saves.txt");
                    saver.Write(0);
                    saver.Close();
                    ToAct();
                }
                else
                {
                    switch (current_boss.Name)
                    {
                        case "Кандзи Романдзи":
                            Action("Кандзи Романдзи: Ты пожалеешь о том что стал охо..тником...");
                            Console.WriteLine("\n(Урон +5)\n(Здоровье +10");
                            Pause();
                            Console.Clear();
                            statsWriter = new StreamWriter("Game/stats.txt");
                            statsWriter.Write(2);
                            statsWriter.Close();
                            Act2();
                            break;
                        case "Демон Ненависти":
                            Action("Демон Ненависти: Они обращаются с неудачниками как с мусором, вы ни как не отличаетесь от нас...");
                            Console.WriteLine("\n(Урон +5)\n(Здоровье +10");
                            Pause();
                            Console.Clear();
                            statsWriter = new StreamWriter("Game/stats.txt");
                            statsWriter.Write(3);
                            statsWriter.Close();
                            Act3();
                            break;
                        case "Обезьяна-страж":
                            Action("Обезьяна-страж: Мой брат покончит с начатым, ты не ОСТАНОВИШЬ ЕГО!!!");
                            Console.WriteLine("\n(Урон +5)\n(Здоровье +10");
                            Pause();
                            Console.Clear();
                            statsWriter = new StreamWriter("Game/stats.txt");
                            statsWriter.Write(4);
                            statsWriter.Close();
                            Act4();
                            break;
                        case "Старшой":
                            Action2("Старшой: Мой руководитель... Кха..Кха.. Он прикончит тебя...");
                            Console.WriteLine("\n(Урон +5)\n(Здоровье +10");
                            Pause();
                            Console.Clear();
                            statsWriter = new StreamWriter("Game/stats.txt");
                            statsWriter.Write(56);
                            statsWriter.Close();
                            Act5();
                            break;
                        case "Плотников-сенсей":
                            Action2("Плотников-сенсей: Кха... Чёрт... Ты сдал мой экзамен! Не ожидал от такого слабака как ты!");
                            Pause();
                            Console.Clear();
                            Console.WriteLine("\n\tПОЗДРАВЛЯЮ!\n\tТЫ ПРОШЕЛ ИГРУ!");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine($"{current_boss.Name} победил\n");

                if (current_boss.Name == "Плотников-сенсей")
                {
                    Action("ХАХАХА... Так и знал что ты неудачник, тебе никогда не одолеть такого профессионала как я!!!");
                    Pause();
                    Console.Clear();
                }

                Console.WriteLine("\nТы погиб\n\t1 - загрузить сохранение\n\t2 - выйти");

                int after_death_choice = Convert.ToInt32(Console.ReadLine());

                if(after_death_choice == 1)
                {
                    saveReader = new StreamReader("Game/saves.txt");
                    if (saveReader.ReadLine().Contains("0"))
                    {
                        Console.WriteLine("Сохранений не найдено\n\nНачать новую игру?");
                        saveReader.Close();
                    }
                    else
                    {
                        Console.Clear();
                        LoadSave();
                    }
                }
                else if(after_death_choice == 2)
                {
                    Process process = new Process();
                }
            }
        }
    }
}
