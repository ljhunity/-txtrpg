using System;
using System.Collections.Generic;

namespace SpartaDungeonRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("여기가 어딘지 모르겠지만 일단 환영합니다!");

            Console.Write("닉네임을 정해주세요 : ");
            string playerName = Console.ReadLine();

            string playerClass = ChooseClass();

            int playerLevel = 1;
            int playerHealth = 1000;
            int playerspeed = 100;
            int playerGold = 1000;
            int playerAttack = 10;
            int playerDefense = 5;
            List<string> inventory = new List<string>();
            HashSet<string> purchasedItems = new HashSet<string>();

            while (true)
            {
                Console.WriteLine("\n무엇을 하시겠습니까?");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.Write("원하시는 행동을 입력해주세요: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ShowStatus(playerName, playerClass, playerLevel, playerAttack, playerDefense, playerHealth, playerGold, playerspeed);
                        break;
                    case "2":
                        ShowInventory(inventory);
                        break;
                    case "3":
                        Shop(ref playerGold, inventory, purchasedItems);
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }

        static string ChooseClass()
        {
            Console.WriteLine("직업을 선택하세요:");
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 마법사");
            Console.WriteLine("3. 궁수");
            Console.WriteLine("4.개발자");

            while (true)
            {
                string choice = Console.ReadLine();
                if (choice == "1") return "전사";
                if (choice == "2") return "마법사";
                if (choice == "3") return "궁수";
                if (choice == "4") return "개발자";

                Console.WriteLine("잘못된 입력입니다. 다시 선택하세요.");
            }
        }

        static void ShowStatus(string name, string playerClass, int level, int attack, int defense, int health, int gold, int playerspeed)
        {
            Console.WriteLine($"\n=== {name}의 상태 ===");
            Console.WriteLine($"레벨: {level} | 이름: {name} | 직업: {playerClass}");
            Console.WriteLine($"공격력: {attack} | 방어력: {defense} | 체력: {health} | 골드: {gold}  스피드: {playerspeed}");
        }

        static void ShowInventory(List<string> inventory)
        {
            Console.WriteLine("\n=== 인벤토리 ===");
            if (inventory.Count == 0)
            {
                Console.WriteLine("인벤토리에 아이템이 없습니다.");
            }
            else
            {
                Console.WriteLine("[아이템 목록]");
                foreach (var item in inventory)
                {
                    Console.WriteLine($"- {item}");
                }
            }
        }

        static void Shop(ref int gold, List<string> inventory, HashSet<string> purchasedItems)
        {
            Console.WriteLine("\n=== 상점 ===");
            Console.WriteLine($"[보유 골드] {gold} G");
            Console.WriteLine("[아이템 목록]");

            var items = new Dictionary<string, (string description, int price)>
            {
                { "수련자 갑옷", ("방어력 +5 | 수련에 도움을 주는 갑옷입니다.", 100) },
                { "무쇠갑옷", ("방어력 +9 | 무쇠로 만들어져 튼튼한 갑옷입니다.", 200) },
                { "스파르타의 갑옷", ("방어력 +15 | 전설의 갑옷입니다.", 350) },
                { "낡은 검", ("공격력 +2 | 쉽게 볼 수 있는 검입니다.", 100) },
                { "청동 도끼", ("공격력 +5 | 사용된 도끼입니다.", 150) },
                { "스파르타의 창", ("공격력 +7 | 전설의 창입니다.", 300) },
                { "제작자의 장난", ("공격력 +999 | 이얍.", 9900) }


            };

            int index = 1;
            foreach (var item in items)
            {
                string status = purchasedItems.Contains(item.Key) ? "구매완료" : $"{item.Value.price} G";
                Console.WriteLine($"- {index++} {item.Key} | {item.Value.description} | {status}");
            }

            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("0. 나가기");
            Console.Write("원하시는 행동을 입력해주세요: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                PurchaseItem(items, ref gold, inventory, purchasedItems);
            }
            else if (choice == "0")
            {
                Console.WriteLine("상점을 종료합니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        static void PurchaseItem(Dictionary<string, (string description, int price)> items, ref int gold, List<string> inventory, HashSet<string> purchasedItems)
        {
            Console.WriteLine("\n아이템 구매를 선택하셨습니다.");
            Console.WriteLine("구매할 아이템을 선택하세요:");

            int index = 1;
            foreach (var item in items)
            {
                if (!purchasedItems.Contains(item.Key))
                {
                    Console.WriteLine($"{index++}. {item.Key}");
                }
            }

            Console.WriteLine("0. 나가기");
            string choice = Console.ReadLine();

            if (choice == "0") return;

            if (int.TryParse(choice, out int itemIndex) && itemIndex > 0 && itemIndex < index)
            {
                var selectedItem = new List<string>(items.Keys)[itemIndex - 1];
                var itemDetails = items[selectedItem];

                if (purchasedItems.Contains(selectedItem))
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                }
                else if (gold >= itemDetails.price)
                {
                    gold -= itemDetails.price;
                    inventory.Add(selectedItem);
                    purchasedItems.Add(selectedItem);
                    Console.WriteLine($"구매를 완료했습니다. {selectedItem}을(를) 인벤토리에 추가했습니다.");
                }
                else
                {
                    Console.WriteLine("Gold가 부족합니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}
