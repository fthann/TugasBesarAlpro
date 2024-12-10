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
    static string connectionString = "Server=localhost;Database=manajemen_parkir;Uid=root;Pwd=;";

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== Manajemen Data Parkir Kelompok 5 ====");
            Console.WriteLine("1. Masukan Data ");
            Console.WriteLine("2. Tampilkan Data parkir");
            Console.WriteLine("3. Edit Data");
            Console.WriteLine("4. Hapus Data");
            Console.WriteLine("5. Cari Data");
            Console.WriteLine("6. Filter Data");
            Console.WriteLine("7. Exit");
            Console.Write("Pilih menu: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    InsertData();
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
                    Console.WriteLine("Keluar dari program.");
                    return;
                default:
                    Console.WriteLine("Pilihan tidak valid. Tekan Enter untuk melanjutkan.");
                    Console.ReadLine();
                    break;
            }
        }
    }


    static int GetNextAvailableID()
{
    int nextID = 1; // Mulai dari ID 1
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        try
        {
            connection.Open();
            string query = "SELECT id FROM data_parkir ORDER BY id ASC"; // Ambil semua ID yang ada
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if ((int)reader["id"] == nextID)
                {
                    nextID++; // Jika ID ada, periksa ID berikutnya
                }
                else
                {
                    break; // Keluar jika menemukan celah (ID kosong)
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while retrieving next ID: {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
    }
    return nextID; // Kembalikan ID yang tersedia
}

    static void InsertData()
{
    int id = GetNextAvailableID(); // Dapatkan ID berikutnya yang tersedia
    Console.Write("Jenis Kendaraan: ");
    
    string jenis = Console.ReadLine();
    Console.Write("Merek Kendaraan: ");

    string merek = Console.ReadLine();
    Console.Write("Nama Kendaraan: ");

    string nama = Console.ReadLine();
    Console.Write("Jumlah: ");
    
    int jumlah = int.Parse(Console.ReadLine());
    Console.Write("Status Kendaraan (Parkir/Keluar): ");
    string status = Console.ReadLine();
    Console.Write("Waktu Mulai: ");
    string waktuMulai = Console.ReadLine();
    Console.Write("Waktu Selesai: ");
    string waktuSelesai = Console.ReadLine();
    Console.Write("Plat Nomor: ");
    string plat = Console.ReadLine();

    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        try
        {
            connection.Open();
            string query = "INSERT INTO data_parkir (id, jenis_kendaraan, merek_kendaraan, nama_kendaraan, jumlah, status_kendaraan, waktu_mulai, waktu_selesai, plat) VALUES (@id, @jenis, @merek, @nama, @jumlah, @status, @waktuMulai, @waktuSelesai, @plat)";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@jenis", jenis);
            cmd.Parameters.AddWithValue("@merek", merek);
            cmd.Parameters.AddWithValue("@nama", nama);
            cmd.Parameters.AddWithValue("@jumlah", jumlah);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@waktuMulai", waktuMulai);
            cmd.Parameters.AddWithValue("@waktuSelesai", waktuSelesai);
            cmd.Parameters.AddWithValue("@plat", plat);

            cmd.ExecuteNonQuery();
            Console.WriteLine("Data berhasil ditambahkan.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
    }
    Console.WriteLine("Tekan Enter untuk melanjutkan.");
    Console.ReadLine();
}


    static void DisplayData()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM data_parkir";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("==== Data Parkir Kelompok 5 ====");
                Console.WriteLine(String.Format("{0,-4} | {1,-15} | {2,-15} | {3,-15} | {4,-7} | {5,-10} | {6,-20} | {7,-20} | {8,-10}",
                    "ID", "Jenis", "Merek", "Nama", "Jumlah", "Status", "Waktu Mulai", "Waktu Selesai", "Plat"));
                Console.WriteLine(new string('-', 112));

                while (reader.Read())
                {
                    Console.WriteLine(String.Format("{0,-4} | {1,-15} | {2,-15} | {3,-15} | {4,-7} | {5,-10} | {6,-20} | {7,-20} | {8,-10}",
                        reader["id"], reader["jenis_kendaraan"], reader["merek_kendaraan"], reader["nama_kendaraan"], reader["jumlah"],
                        reader["status_kendaraan"], reader["waktu_mulai"], reader["waktu_selesai"], reader["plat"]));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
        Console.WriteLine("Tekan Enter untuk melanjutkan.");
        Console.ReadLine();
    }

    static void UpdateData()
    {
        DisplayData();
        Console.Write("Masukkan ID data yang ingin diupdate: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Jenis Kendaraan Baru: ");
        string jenis = Console.ReadLine();
        Console.Write("Merek Kendaraan Baru: ");
        string merek = Console.ReadLine();
        Console.Write("Nama Kendaraan Baru: ");
        string nama = Console.ReadLine();
        Console.Write("Jumlah Baru: ");
        int jumlah = int.Parse(Console.ReadLine());
        Console.Write("Status Kendaraan Baru (Parkir/Selesai): ");
        string status = Console.ReadLine();
        Console.Write("Waktu Mulai Baru: ");
        string waktuMulai = Console.ReadLine();
        Console.Write("Waktu Selesai Baru: ");
        string waktuSelesai = Console.ReadLine();
        Console.Write("Plat Nomor Baru: ");
        string plat = Console.ReadLine();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "UPDATE data_parkir SET jenis_kendaraan=@jenis, merek_kendaraan=@merek, nama_kendaraan=@nama, jumlah=@jumlah, status_kendaraan=@status, waktu_mulai=@waktuMulai, waktu_selesai=@waktuSelesai, plat=@plat WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@jenis", jenis);
                cmd.Parameters.AddWithValue("@merek", merek);
                cmd.Parameters.AddWithValue("@nama", nama);
                cmd.Parameters.AddWithValue("@jumlah", jumlah);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@waktuMulai", waktuMulai);
                cmd.Parameters.AddWithValue("@waktuSelesai", waktuSelesai);
                cmd.Parameters.AddWithValue("@plat", plat);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Data berhasil diupdate.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
        Console.WriteLine("Tekan Enter untuk melanjutkan.");
        Console.ReadLine();
    }

    static void DeleteData()
    {
        DisplayData();
        Console.Write("Masukkan ID data yang ingin dihapus: ");
        int id = int.Parse(Console.ReadLine());

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM data_parkir WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Data berhasil dihapus.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
        Console.WriteLine("Tekan Enter untuk melanjutkan.");
        Console.ReadLine();
    }

    static void SearchData()
    {
        Console.Write("Masukkan kata kunci untuk pencarian: ");
        string keyword = Console.ReadLine();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM data_parkir WHERE jenis_kendaraan LIKE @keyword OR nama_kendaraan LIKE @keyword OR plat LIKE @keyword";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");
                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("==== Hasil Pencarian ====");
                Console.WriteLine(String.Format("{0,-4} | {1,-15} | {2,-15} | {3,-15} | {4,-7} | {5,-10} | {6,-20} | {7,-20} | {8,-10}",
                    "ID", "Jenis", "Merek", "Nama", "Jumlah", "Status", "Waktu Mulai", "Waktu Selesai", "Plat"));
                Console.WriteLine(new string('-', 112));

                while (reader.Read())
                {
                    Console.WriteLine(String.Format("{0,-4} | {1,-15} | {2,-15} | {3,-15} | {4,-7} | {5,-10} | {6,-20} | {7,-20} | {8,-10}",
                        reader["id"], reader["jenis_kendaraan"], reader["merek_kendaraan"], reader["nama_kendaraan"], reader["jumlah"],
                        reader["status_kendaraan"], reader["waktu_mulai"], reader["waktu_selesai"], reader["plat"]));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
        Console.WriteLine("Tekan Enter untuk melanjutkan.");
        Console.ReadLine();
    }

    static void FilterData()
    {
        Console.Write("Masukkan status kendaraan untuk filter (Parkir/Keluar): ");
        string status = Console.ReadLine();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM data_parkir WHERE status_kendaraan=@status";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@status", status);
                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("==== Hasil Filter ====");
                Console.WriteLine(String.Format("{0,-4} | {1,-15} | {2,-15} | {3,-15} | {4,-7} | {5,-10} | {6,-20} | {7,-20} | {8,-10}",
                    "ID", "Jenis", "Merek", "Nama", "Jumlah", "Status", "Waktu Mulai", "Waktu Selesai", "Plat"));
                Console.WriteLine(new string('-', 112));

                while (reader.Read())
                {
                    Console.WriteLine(String.Format("{0,-4} | {1,-15} | {2,-15} | {3,-15} | {4,-7} | {5,-10} | {6,-20} | {7,-20} | {8,-10}",
                        reader["id"], reader["jenis_kendaraan"], reader["merek_kendaraan"], reader["nama_kendaraan"], reader["jumlah"],
                        reader["status_kendaraan"], reader["waktu_mulai"], reader["waktu_selesai"], reader["plat"]));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
        Console.WriteLine("Tekan Enter untuk melanjutkan.");
        Console.ReadLine();
    }
}
