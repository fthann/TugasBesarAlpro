// // # Kelas: SI-24-02
// // # Kelompok: 05
// // # Anggota Kelompok: 
// // # 1. Muhammad Umar Fathan Alfaruq (102042400095) 
// // # 2. Desti Mutiara Anggun (102042400099)
// // # 3. I Komang Arya W.T.W (102042400169)
// // # 4. Fatihul Chaira (102042400005)

using System;
using MySql.Data.MySqlClient;

public class Database_0205
{
    private static string connectionString = "Server=localhost;Database=ParkirDB;User=root;Password=;";

    public static MySqlConnection GetConnection()
    {
        MySqlConnection conn = new MySqlConnection(connectionString);
        return conn;
    }
}

public class Program
{
    // memakai boolean untuk memvalidasi input hanya teks
    public static bool IsTextValid(string input)
    {
        foreach (char c in input)
        {
            if (Char.IsDigit(c))  
            {
                return false; 
            }
        }
        return true; 
    }

    // fungsi Insert Data dengan validasi input
    public static bool InsertData_0205(string jenisKendaraan, string merekKendaraan, string namaKendaraan, int jumlah, string statusKendaraan, DateTime waktuMulai, DateTime waktuSelesai, string plat)
    {
        try
        {
            // Validasi input untuk jenis, merek, nama, dan status kendaraan tidak boleh mengandung angka
            if (!IsTextValid(jenisKendaraan) || !IsTextValid(merekKendaraan) || !IsTextValid(namaKendaraan) || !IsTextValid(statusKendaraan))
            {
                Console.WriteLine("Error: Jenis, Merek, Nama, dan Status kendaraan tidak boleh mengandung angka.");
                return false;
            }

            using (var conn = Database_0205.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO parkir (jenis_kendaraan, merek_kendaraan, nama_kendaraan, jumlah, status_kendaraan, waktu_mulai, waktu_selesai, plat) " +
                               "VALUES (@jenisKendaraan, @merekKendaraan, @namaKendaraan, @jumlah, @statusKendaraan, @waktuMulai, @waktuSelesai, @plat)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@jenisKendaraan", jenisKendaraan);
                cmd.Parameters.AddWithValue("@merekKendaraan", merekKendaraan);
                cmd.Parameters.AddWithValue("@namaKendaraan", namaKendaraan);
                cmd.Parameters.AddWithValue("@jumlah", jumlah);
                cmd.Parameters.AddWithValue("@statusKendaraan", statusKendaraan);
                cmd.Parameters.AddWithValue("@waktuMulai", waktuMulai);
                cmd.Parameters.AddWithValue("@waktuSelesai", waktuSelesai);
                cmd.Parameters.AddWithValue("@plat", plat);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return false;
        }
    }

    // menampilkan data parkir
    public static void DisplayData_0205(string filterType = null, string filterValue = null)
    {
        try
        {
            using (var conn = Database_0205.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM parkir";
                
                if (filterType != null && filterValue != null)
                {
                    query += " WHERE " + filterType + " LIKE @filterValue";
                }

                MySqlCommand cmd = new MySqlCommand(query, conn);

                if (filterType != null && filterValue != null)
                {
                    cmd.Parameters.AddWithValue("@filterValue", "%" + filterValue + "%");
                }

                MySqlDataReader reader = cmd.ExecuteReader();

                // Header tabel
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");
                Console.WriteLine("| {0,-4} | {1,-18} | {2,-18} | {3,-25} | {4,-6} | {5,-15} | {6,-20} | {7,-20} | {8,-12} |", 
                    "ID", "Jenis Kendaraan", "Merek Kendaraan", "Nama Kendaraan", "Jumlah", "Status Kendaraan", "Waktu Mulai", "Waktu Selesai", "Plat");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");

                while (reader.Read())
                {
                    Console.WriteLine("| {0,-4} | {1,-18} | {2,-18} | {3,-25} | {4,-6} | {5,-15} | {6,-20} | {7,-20} | {8,-12} |", 
                        reader["id"], 
                        reader["jenis_kendaraan"], 
                        reader["merek_kendaraan"], 
                        reader["nama_kendaraan"], 
                        reader["jumlah"], 
                        reader["status_kendaraan"], 
                        reader["waktu_mulai"], 
                        reader["waktu_selesai"], 
                        reader["plat"]);
                }
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    // fungsi untuk update data parkir
    public static bool UpdateData_0205(int id, string jenisKendaraan, string merekKendaraan, string namaKendaraan, int jumlah, string statusKendaraan, DateTime waktuMulai, DateTime waktuSelesai, string plat)
    {
        try
        {
            // Validasi input untuk jenis, merek, nama, dan status kendaraan tidak boleh mengandung angka
            if (!IsTextValid(jenisKendaraan) || !IsTextValid(merekKendaraan) || !IsTextValid(namaKendaraan) || !IsTextValid(statusKendaraan))
            {
                Console.WriteLine("Error: Jenis, Merek, Nama, dan Status kendaraan tidak boleh mengandung angka.");
                return false;
            }

            using (var conn = Database_0205.GetConnection())
            {
                conn.Open();
                string query = "UPDATE parkir SET jenis_kendaraan = @jenisKendaraan, merek_kendaraan = @merekKendaraan, nama_kendaraan = @namaKendaraan, jumlah = @jumlah, " +
                               "status_kendaraan = @statusKendaraan, waktu_mulai = @waktuMulai, waktu_selesai = @waktuSelesai, plat = @plat WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@jenisKendaraan", jenisKendaraan);
                cmd.Parameters.AddWithValue("@merekKendaraan", merekKendaraan);
                cmd.Parameters.AddWithValue("@namaKendaraan", namaKendaraan);
                cmd.Parameters.AddWithValue("@jumlah", jumlah);
                cmd.Parameters.AddWithValue("@statusKendaraan", statusKendaraan);
                cmd.Parameters.AddWithValue("@waktuMulai", waktuMulai);
                cmd.Parameters.AddWithValue("@waktuSelesai", waktuSelesai);
                cmd.Parameters.AddWithValue("@plat", plat);
                cmd.Parameters.AddWithValue("@id", id);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return false;
        }
    }

    // fungsi untuk menghapus data parkir
    public static bool DeleteData_0205(int id)
    {
        try
        {
            using (var conn = Database_0205.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM parkir WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return false;
        }
    }

    // fungsi untuk keluar dari program
    public static void ExitProgram_0205()
    {
        Console.WriteLine("Terima kasih telah menggunakan aplikasi!");
        Environment.Exit(0);
    }

    // fungsi pencarian data parkir
    public static void SearchData_0205()
    {
        Console.WriteLine("Pencarian data parkir berdasarkan:");
        Console.WriteLine("1. ID");
        Console.WriteLine("2. Nama Kendaraan");
        Console.WriteLine("3. Plat Kendaraan");
        Console.Write("Pilih kriteria pencarian: ");
        int searchOption = int.Parse(Console.ReadLine());

        string searchColumn = string.Empty;
        string searchValue = string.Empty;

        switch (searchOption)
        {
            case 1:
                Console.Write("Masukkan ID Parkir: ");
                searchColumn = "id";
                break;
            case 2:
                Console.Write("Masukkan Nama Kendaraan: ");
                searchColumn = "nama_kendaraan";
                break;
            case 3:
                Console.Write("Masukkan Plat Kendaraan: ");
                searchColumn = "plat";
                break;
            default:
                Console.WriteLine("Pilihan tidak valid.");
                return;
        }

        searchValue = Console.ReadLine();
        DisplayData_0205(searchColumn, searchValue);
    }

    // fungsi filter data parkir
    public static void FilterData_0205()
    {
        Console.WriteLine("Filter data parkir berdasarkan:");
        Console.WriteLine("1. Status Kendaraan");
        Console.WriteLine("2. Jenis Kendaraan");
        Console.Write("Pilih kriteria filter: ");
        int filterOption = int.Parse(Console.ReadLine());

        string filterColumn = string.Empty;
        string filterValue = string.Empty;

        switch (filterOption)
        {
            case 1:
                Console.Write("Masukkan Status Kendaraan (misal: aktif, tidak aktif): ");
                filterColumn = "status_kendaraan";
                break;
            case 2:
                Console.Write("Masukkan Jenis Kendaraan (misal: mobil, motor): ");
                filterColumn = "jenis_kendaraan";
                break;
            default:
                Console.WriteLine("Pilihan tidak valid.");
                return;
        }

        filterValue = Console.ReadLine();
        DisplayData_0205(filterColumn, filterValue);
    }

    // main Program untuk Menampilkan Menu
    public static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Masukkan Data Parkir");
            Console.WriteLine("2. Tampilkan Data Parkir");
            Console.WriteLine("3. Update Data Parkir");
            Console.WriteLine("4. Hapus Data Parkir");
            Console.WriteLine("5. Pencarian Data Parkir");
            Console.WriteLine("6. Filter Data Parkir");
            Console.WriteLine("7. Keluar");
            Console.Write("Pilih menu: ");
            int pilihan = int.Parse(Console.ReadLine());

            switch (pilihan)
            {
                case 1:
                    Console.WriteLine("Masukkan data parkir:");
                    Console.Write("Jenis Kendaraan: ");
                    string jenisKendaraan = Console.ReadLine();
                    Console.Write("Merek Kendaraan: ");
                    string merekKendaraan = Console.ReadLine();
                    Console.Write("Nama Kendaraan: ");
                    string namaKendaraan = Console.ReadLine();
                    Console.Write("Status Kendaraan: ");
                    string statusKendaraan = Console.ReadLine();

                    // Validasi Jenis, Merek, Nama, dan Status kendaraan
                    bool isValid = false;
                    while (!isValid)
                    {
                        if (IsTextValid(jenisKendaraan) && IsTextValid(merekKendaraan) && IsTextValid(namaKendaraan) && IsTextValid(statusKendaraan))
                        {
                            isValid = true;
                        }
                        else
                        {
                            Console.WriteLine("Error: Jenis, Merek, Nama, dan Status kendaraan tidak boleh mengandung angka.");
                            Console.Write("Jenis Kendaraan: ");
                            jenisKendaraan = Console.ReadLine();
                            Console.Write("Merek Kendaraan: ");
                            merekKendaraan = Console.ReadLine();
                            Console.Write("Nama Kendaraan: ");
                            namaKendaraan = Console.ReadLine();
                            Console.Write("Status Kendaraan: ");
                            statusKendaraan = Console.ReadLine();
                        }
                    }

                    Console.Write("Jumlah: ");
                    int jumlah = int.Parse(Console.ReadLine());
                    Console.Write("Waktu Mulai Parkir (yyyy-mm-dd hh:mm:ss): ");
                    DateTime waktuMulai = DateTime.Parse(Console.ReadLine());
                    Console.Write("Waktu Selesai Parkir (yyyy-mm-dd hh:mm:ss): ");
                    DateTime waktuSelesai = DateTime.Parse(Console.ReadLine());
                    Console.Write("Plat Kendaraan: ");
                    string plat = Console.ReadLine();

                    if (InsertData_0205(jenisKendaraan, merekKendaraan, namaKendaraan, jumlah, statusKendaraan, waktuMulai, waktuSelesai, plat))
                    {
                        Console.WriteLine("Data berhasil dimasukkan.");
                    }
                    else
                    {
                        Console.WriteLine("Gagal memasukkan data.");
                    }
                    break;

                case 2:
                    Console.WriteLine("Tampilkan Data Parkir:");
                    DisplayData_0205();
                    break;

                case 3:
                    Console.Write("Masukkan ID Data Parkir yang ingin diupdate: ");
                    int idUpdate = int.Parse(Console.ReadLine());
                    Console.WriteLine("Masukkan data parkir baru:");
                    Console.Write("Jenis Kendaraan: ");
                    jenisKendaraan = Console.ReadLine();
                    Console.Write("Merek Kendaraan: ");
                    merekKendaraan = Console.ReadLine();
                    Console.Write("Nama Kendaraan: ");
                    namaKendaraan = Console.ReadLine();
                    Console.Write("Status Kendaraan: ");
                    statusKendaraan = Console.ReadLine();
                    Console.Write("Jumlah: ");
                    jumlah = int.Parse(Console.ReadLine());
                    Console.Write("Waktu Mulai Parkir (yyyy-mm-dd hh:mm:ss): ");
                    waktuMulai = DateTime.Parse(Console.ReadLine());
                    Console.Write("Waktu Selesai Parkir (yyyy-mm-dd hh:mm:ss): ");
                    waktuSelesai = DateTime.Parse(Console.ReadLine());
                    Console.Write("Plat Kendaraan: ");
                    plat = Console.ReadLine();

                    if (UpdateData_0205(idUpdate, jenisKendaraan, merekKendaraan, namaKendaraan, jumlah, statusKendaraan, waktuMulai, waktuSelesai, plat))
                    {
                        Console.WriteLine("Data berhasil diupdate.");
                    }
                    else
                    {
                        Console.WriteLine("Gagal mengupdate data.");
                    }
                    break;

                case 4:
                    Console.Write("Masukkan ID Data Parkir yang ingin dihapus: ");
                    int idDelete = int.Parse(Console.ReadLine());

                    if (DeleteData_0205(idDelete))
                    {
                        Console.WriteLine("Data berhasil dihapus.");
                    }
                    else
                    {
                        Console.WriteLine("Gagal menghapus data.");
                    }
                    break;

                case 5:
                    SearchData_0205();
                    break;

                case 6:
                    FilterData_0205();
                    break;

                case 7:
                    ExitProgram_0205();
                    break;

                default:
                    Console.WriteLine("Pilihan tidak valid.");
                    break;
            }
        }
    }
}
