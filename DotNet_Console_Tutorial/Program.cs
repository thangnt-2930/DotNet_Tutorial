using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<string> students = new List<string>();
        List<string> khoaHoc = new List<string>();
        Dictionary<string, List<string>> dsGhiDanh = new Dictionary<string, List<string>>();


        while (true)
        {
            Console.WriteLine("Hay chon thao tac ban muon:");
            Console.WriteLine("1. Them sinh vien.");
            Console.WriteLine("2. Them khoa hoc.");
            Console.WriteLine("3. Ghi danh.");
            Console.WriteLine("4. Thoat.");
            string action = Console.ReadLine();

            switch(action)
            {
                case "1":
                    menuThemSV(students);
                    break;
                case "2":
                    menuThemKhoaHoc(khoaHoc);
                    break;
                case "3":
                    menuGhiDanh(dsGhiDanh, khoaHoc);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Lua chon khong hop le. Vui long chon lai.");
                    break;
            }
        }
    }

    static void menuThemSV(List<string> students) 
    {
        while (true)
        {
            Console.WriteLine("1. Them sinh vien:");
            Console.WriteLine("2. Danh sach sinh vien:");
            Console.WriteLine("3. Tro ve menu chinh");

            string action = Console.ReadLine();
            switch (action)
            {
                case "1":
                    themSV(students);
                    break;
                case "2":
                    danhsachSV(students);
                    break;
                case "3":
                    return; 
                default:
                    Console.WriteLine("Lua chon khong hop le. Vui long chon lai.");
                    break;
            }
        }
    }

    static void themSV(List<string> students) 
    {
        Console.WriteLine("1. Nhap ten sinh vien:");
        string tenSinhVien = Console.ReadLine();
        students.Add(tenSinhVien);
    }

    static void danhsachSV(List<string> students) 
    {
        foreach (string tenSV in students)
        {
            Console.WriteLine($"- {tenSV}");
        }
    }

    static void menuThemKhoaHoc(List<string> khoaHoc) 
    {
        while (true)
        {
            Console.WriteLine("1. Nhap ten khoa hoc:");
            Console.WriteLine("2. Danh sach khoa hoc:");
            Console.WriteLine("3. Tro ve menu chinh");
            string action = Console.ReadLine();
            switch (action)
            {
                case "1":
                    themKhoaHoc(khoaHoc);
                    break;
                case "2":
                    danhsachKhoaHoc(khoaHoc);
                    break;
                case "3":
                    return; 
                default:
                    Console.WriteLine("Lua chon khong hop le. Vui long chon lai.");
                    break;
            }
        }
    }

    static void themKhoaHoc(List<string> khoaHoc) 
    {
        Console.WriteLine("1. Nhap ten khoa hoc:");
        string tenSinhVien = Console.ReadLine();
        khoaHoc.Add(tenSinhVien);
    }

    static void danhsachKhoaHoc(List<string> khoaHoc) 
    {
        foreach (string tenKhoaHoc in khoaHoc)
        {
            Console.WriteLine(tenKhoaHoc);
        }
    }

    static void menuGhiDanh(Dictionary<string, List<string>> dsGhiDanh, List<string> khoaHoc) 
    {
        while (true)
        {
            Console.WriteLine("1. Xem danh sach khoa hoc.");
            Console.WriteLine("2. Nhap ten khoa hoc muon dang ki:");
            Console.WriteLine("3. Xem danh sach ghi danh:");
            Console.WriteLine("4. Tro ve menu chinh");
            string action = Console.ReadLine();
            switch (action)
            {
                case "1":
                    danhsachKhoaHoc(khoaHoc);
                    break;
                case "2":
                    ghiDanh(dsGhiDanh, khoaHoc);
                    break;
                case "3":
                    danhsachGhiDanh(dsGhiDanh);
                    break;
                case "4":
                    return; 
                default:
                    Console.WriteLine("Lua chon khong hop le. Vui long chon lai.");
                    break;
            }
        }
    }

    static void ghiDanh(Dictionary<string, List<string>> dsGhiDanh, List<string> khoaHoc) 
    {
        Console.WriteLine("1. Nhap ten khoa hoc ban muon ghi danh:");
        string tenKhoaHoc = Console.ReadLine();
        if (!dsGhiDanh.ContainsKey(tenKhoaHoc))
        {
            dsGhiDanh[tenKhoaHoc] = new List<string>();
        }

        Console.WriteLine("2. Nhap ten cua ban:");
        string tenSV = Console.ReadLine();
        dsGhiDanh[tenKhoaHoc].Add(tenSV);
    }

    static void danhsachGhiDanh(Dictionary<string, List<string>> dsGhiDanh) 
    {
        foreach (var item in dsGhiDanh)
        {
            Console.WriteLine($"Khoa hoc: {item.Key}");
            foreach (var sv in item.Value)
            {
                Console.WriteLine($"- {sv}");
            }
        }
    }
}