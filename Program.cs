// # Kelas: SI-24-02
// # Kelompok: 05
// # Anggota Kelompok: 
// # 1. Muhammad Umar Fathan Alfaruq (102042400095) 
// # 2. Desti Mutiara Anggun (102042400099)
// # 3. I Komang Arya W.T.W (102042400169)
// # 4. Fatihul Chaira (102042400005)

using System;
using MySql.Data.MySqlClient;

class Program
{
    static string connectionString = "Server=localhost;Database=parkir_db;Uid=root;Pwd=;";
    
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Manajemen Data Parkir ===");
            Console.WriteLine("1. Masukkan Data Parkir");
            Console.WriteLine("2. Tampilkan Data Parkir");
            Console.WriteLine("3. Update Data Parkir");
            Console.WriteLine("4. Hapus Data Parkir");
            Console.WriteLine("5. Cari Data Parkir");
            Console.WriteLine("6. Filter Data Parkir");
            Console.WriteLine("7. Keluar Program");
            Console.Write("Pilih menu (1-7): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddData();
                    break;
                case "2":
                    DisplayData();
                    break;
                case "3":
                    UpdateData();
                    break;
                case "4":
                    DeleteData();
                    break;
                case "5":
                    SearchData();
                    break;
                case "6":
                    FilterData();
                    break;
                case "7":
                    Console.WriteLine("Keluar Program...");
                    return;
                default:
                    Console.WriteLine("Pilihan tidak valid!");
                    break;
            }

            Console.WriteLine("\nTekan Enter untuk kembali ke menu...");
            Console.ReadLine();
        }
    }

    static void AddData()
    {
        try
        {
            Console.Write("Jenis Kendaraan: ");
            string jenis = Console.ReadLine();

            Console.Write("Merek Kendaraan: ");
            string merek = Console.ReadLine();

            Console.Write("Nama Kendaraan: ");
            string nama = Console.ReadLine();

            Console.Write("Jumlah: ");
            int jumlah = int.Parse(Console.ReadLine());

            Console.Write("Status Kendaraan: ");
            string status = Console.ReadLine();

            Console.Write("Waktu Mulai (yyyy-mm-dd hh:mm:ss): ");
            DateTime waktuMulai = DateTime.Parse(Console.ReadLine());

            Console.Write("Waktu Selesai (yyyy-mm-dd hh:mm:ss): ");
            DateTime waktuSelesai = DateTime.Parse(Console.ReadLine());

            Console.Write("Plat Nomor: ");
            string plat = Console.ReadLine();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO data_parkir (jenis_kendaraan, merek_kendaraan, nama_kendaraan, jumlah, status_kendaraan, waktu_mulai, waktu_selesai, plat) " +
                               "VALUES (@jenis, @merek, @nama, @jumlah, @status, @waktuMulai, @waktuSelesai, @plat)";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@jenis", jenis);
                    cmd.Parameters.AddWithValue("@merek", merek);
                    cmd.Parameters.AddWithValue("@nama", nama);
                    cmd.Parameters.AddWithValue("@jumlah", jumlah);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@waktuMulai", waktuMulai);
                    cmd.Parameters.AddWithValue("@waktuSelesai", waktuSelesai);
                    cmd.Parameters.AddWithValue("@plat", plat);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Data berhasil ditambahkan!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
        }
    }

    static void DisplayData()
{
    try
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM data_parkir";

            using (var cmd = new MySqlCommand(query, connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    // Set lebar kolom
                    int[] columnWidths = { 5, 15, 15, 20, 8, 12, 19, 19, 12 };
                    string[] headers = { "ID", "Jenis", "Merek", "Nama", "Jumlah", "Status", "Waktu Mulai", "Waktu Selesai", "Plat" };

                    // Cetak header tabel
                    PrintTableLine(columnWidths);
                    PrintRow(headers, columnWidths);
                    PrintTableLine(columnWidths);

                    // Cetak data
                    while (reader.Read())
                    {
                        string[] rowData = {
                            reader["id"].ToString(),
                            reader["jenis_kendaraan"].ToString(),
                            reader["merek_kendaraan"].ToString(),
                            reader["nama_kendaraan"].ToString(),
                            reader["jumlah"].ToString(),
                            reader["status_kendaraan"].ToString(),
                            reader["waktu_mulai"].ToString(),
                            reader["waktu_selesai"].ToString(),
                            reader["plat"].ToString()
                        };
                        PrintRow(rowData, columnWidths);
                    }

                    // Cetak garis akhir tabel
                    PrintTableLine(columnWidths);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
    }
}

    // Fungsi mencetak garis tabel
    static void PrintTableLine(int[] columnWidths)
{
    foreach (var width in columnWidths)
    {
        Console.Write("+");
        Console.Write(new string('-', width));
    }
    Console.WriteLine("+");
}

    // Fungsi mencetak satu baris data
    static void PrintRow(string[] rowData, int[] columnWidths)
{
    for (int i = 0; i < rowData.Length; i++)
    {
        Console.Write("|");
        Console.Write(rowData[i].PadRight(columnWidths[i]));
    }
    Console.WriteLine("|");
}


    static void UpdateData()
{
    try
    {
        Console.Write("Masukkan ID data yang ingin diupdate: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Jenis Kendaraan: ");
        string jenis = Console.ReadLine();

        Console.Write("Merek Kendaraan: ");
        string merek = Console.ReadLine();

        Console.Write("Nama Kendaraan: ");
        string nama = Console.ReadLine();

        Console.Write("Jumlah: ");
        int jumlah = int.Parse(Console.ReadLine());

        Console.Write("Status Kendaraan: ");
        string status = Console.ReadLine();

        Console.Write("Waktu Mulai (yyyy-mm-dd hh:mm:ss): ");
        DateTime waktuMulai = DateTime.Parse(Console.ReadLine());

        Console.Write("Waktu Selesai (yyyy-mm-dd hh:mm:ss): ");
        DateTime waktuSelesai = DateTime.Parse(Console.ReadLine());

        Console.Write("Plat Nomor: ");
        string plat = Console.ReadLine();

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "UPDATE data_parkir SET jenis_kendaraan=@jenis, merek_kendaraan=@merek, nama_kendaraan=@nama, jumlah=@jumlah, status_kendaraan=@status, waktu_mulai=@waktuMulai, waktu_selesai=@waktuSelesai, plat=@plat WHERE id=@id";

            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@jenis", jenis);
                cmd.Parameters.AddWithValue("@merek", merek);
                cmd.Parameters.AddWithValue("@nama", nama);
                cmd.Parameters.AddWithValue("@jumlah", jumlah);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@waktuMulai", waktuMulai);
                cmd.Parameters.AddWithValue("@waktuSelesai", waktuSelesai);
                cmd.Parameters.AddWithValue("@plat", plat);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Data berhasil diupdate!");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
    }
}


    static void DeleteData()
{
    try
    {
        Console.Write("Masukkan ID data yang ingin dihapus: ");
        int id = int.Parse(Console.ReadLine());

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "DELETE FROM data_parkir WHERE id=@id";

            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Data berhasil dihapus!");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
    }
}

    static void SearchData()
{
    try
    {
        Console.Write("Masukkan kata kunci untuk mencari (contoh: plat, merek, nama): ");
        string keyword = Console.ReadLine();

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM data_parkir WHERE plat LIKE @keyword OR merek_kendaraan LIKE @keyword OR nama_kendaraan LIKE @keyword";

            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\n=== Hasil Pencarian ===");
                    Console.WriteLine("ID | Jenis | Merek | Nama | Jumlah | Status | Mulai | Selesai | Plat");

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["id"]} | {reader["jenis_kendaraan"]} | {reader["merek_kendaraan"]} | {reader["nama_kendaraan"]} | {reader["jumlah"]} | {reader["status_kendaraan"]} | {reader["waktu_mulai"]} | {reader["waktu_selesai"]} | {reader["plat"]}");
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
    }
}

    static void FilterData()
{
    try
    {
        Console.WriteLine("Pilih filter: ");
        Console.WriteLine("1. Berdasarkan Jenis Kendaraan");
        Console.WriteLine("2. Berdasarkan Status Kendaraan");
        Console.Write("Pilihan (1/2): ");
        string filterChoice = Console.ReadLine();

        string column = "";
        if (filterChoice == "1") column = "jenis_kendaraan";
        else if (filterChoice == "2") column = "status_kendaraan";
        else
        {
            Console.WriteLine("Pilihan filter tidak valid!");
            return;
        }

        Console.Write($"Masukkan nilai untuk filter ({column}): ");
        string filterValue = Console.ReadLine();

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = $"SELECT * FROM data_parkir WHERE {column}=@value";

            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@value", filterValue);

                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\n=== Hasil Filter ===");
                    Console.WriteLine("ID | Jenis | Merek | Nama | Jumlah | Status | Mulai | Selesai | Plat");

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["id"]} | {reader["jenis_kendaraan"]} | {reader["merek_kendaraan"]} | {reader["nama_kendaraan"]} | {reader["jumlah"]} | {reader["status_kendaraan"]} | {reader["waktu_mulai"]} | {reader["waktu_selesai"]} | {reader["plat"]}");
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
    }
}

}
