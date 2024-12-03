using System;
using MySql.Data.MySqlClient;

class Program
{
    static MySqlConnection connection = new MySqlConnection("Server=localhost;Database=parkir_db;Uid=root;Pwd=;");

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n=== Menu Manajemen Data Parkir ===");
            Console.WriteLine("1. Masukan Data");
            Console.WriteLine("2. Tampilkan Data parkir");
            Console.WriteLine("3. Edit Data parkir");
            Console.WriteLine("4. Hapus Data parkir");
            Console.WriteLine("5. Search Data parkir");
            Console.WriteLine("6. Filter Data parkir");
            Console.WriteLine("7. Keluar Program");
            Console.Write("Pilih opsi (1-7): ");
            
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
                    Console.WriteLine("Program selesai. Terima kasih!");
                    return;
                default:
                    Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                    break;
            }
        }
    }

    static void InsertData()
    {
        try
        {
            connection.Open();

            Console.Write("Jenis Kendaraan: ");
            string jenis = Console.ReadLine();
            Console.Write("Merek Kendaraan: ");
            string merek = Console.ReadLine();
            Console.Write("Nama Kendaraan: ");
            string nama = Console.ReadLine();
            Console.Write("Jumlah Kendaraan: ");
            int jumlah = int.Parse(Console.ReadLine());
            Console.Write("Status Kendaraan: ");
            string status = Console.ReadLine();
            Console.Write("Waktu Mulai: ");
            string waktuMulai = Console.ReadLine();
            Console.Write("Waktu Selesai: ");
            string waktuSelesai = Console.ReadLine();
            Console.Write("Plat Kendaraan: ");
            string plat = Console.ReadLine();

            string insertQuery = @"
                INSERT INTO data_parkir2 (jenis_kendaraan, merek_kendaraan, nama_kendaraan, jumlah, status_kendaraan, waktu_mulai, waktu_selesai, plat)
                VALUES (@jenis, @merek, @nama, @jumlah, @status, @waktuMulai, @waktuSelesai, @plat)";
            MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
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
        catch (Exception ex)
        {
            Console.WriteLine("Kesalahan: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }

    static void DisplayData()
    {
        try
        {
            connection.Open();

            string selectQuery = "SELECT * FROM data_parkir2 ORDER BY id";
            MySqlCommand cmd = new MySqlCommand(selectQuery, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n=== Data Parkir ===");
            Console.WriteLine("| ID  | Jenis    | Merek     | Nama       | Jumlah | Status    | Waktu Mulai      | Waktu Selesai   | Plat      |");
            Console.WriteLine(new string('-', 95));
            while (reader.Read())
            {
                Console.WriteLine($"| {reader["id"],-4} | {reader["jenis_kendaraan"],-8} | {reader["merek_kendaraan"],-9} | {reader["nama_kendaraan"],-10} | {reader["jumlah"],-6} | {reader["status_kendaraan"],-9} | {reader["waktu_mulai"],-15} | {reader["waktu_selesai"],-15} | {reader["plat"],-8} |");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Kesalahan: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }

    static void UpdateData()
    {
        try
        {
            connection.Open();

            DisplayData();
            Console.Write("\nMasukkan ID yang ingin diupdate: ");
            int id = int.Parse(Console.ReadLine());

            string checkQuery = "SELECT COUNT(*) FROM data_parkir2 WHERE id = @id";
            MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection);
            checkCmd.Parameters.AddWithValue("@id", id);
            int count = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (count == 0)
            {
                Console.WriteLine("ID tidak ditemukan.");
            }
            else
            {
                Console.Write("Jenis Kendaraan Baru: ");
                string jenis = Console.ReadLine();
                Console.Write("Merek Kendaraan Baru: ");
                string merek = Console.ReadLine();
                Console.Write("Nama Kendaraan Baru: ");
                string nama = Console.ReadLine();
                Console.Write("Jumlah Kendaraan Baru: ");
                int jumlah = int.Parse(Console.ReadLine());
                Console.Write("Status Kendaraan Baru: ");
                string status = Console.ReadLine();
                Console.Write("Waktu Mulai Baru: ");
                string waktuMulai = Console.ReadLine();
                Console.Write("Waktu Selesai Baru: ");
                string waktuSelesai = Console.ReadLine();
                Console.Write("Plat Kendaraan Baru: ");
                string plat = Console.ReadLine();

                string updateQuery = @"
                    UPDATE data_parkir2
                    SET jenis_kendaraan = @jenis, merek_kendaraan = @merek, nama_kendaraan = @nama,
                        jumlah = @jumlah, status_kendaraan = @status, waktu_mulai = @waktuMulai,
                        waktu_selesai = @waktuSelesai, plat = @plat
                    WHERE id = @id";
                MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection);
                updateCmd.Parameters.AddWithValue("@jenis", jenis);
                updateCmd.Parameters.AddWithValue("@merek", merek);
                updateCmd.Parameters.AddWithValue("@nama", nama);
                updateCmd.Parameters.AddWithValue("@jumlah", jumlah);
                updateCmd.Parameters.AddWithValue("@status", status);
                updateCmd.Parameters.AddWithValue("@waktuMulai", waktuMulai);
                updateCmd.Parameters.AddWithValue("@waktuSelesai", waktuSelesai);
                updateCmd.Parameters.AddWithValue("@plat", plat);
                updateCmd.Parameters.AddWithValue("@id", id);

                updateCmd.ExecuteNonQuery();
                Console.WriteLine("Data berhasil diupdate!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Kesalahan: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }

    static void DeleteData()
    {
        try
        {
            connection.Open();

            DisplayData();
            Console.Write("\nMasukkan ID yang ingin dihapus: ");
            int id = int.Parse(Console.ReadLine());

            string deleteQuery = "DELETE FROM data_parkir2 WHERE id = @id";
            MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, connection);
            deleteCmd.Parameters.AddWithValue("@id", id);

            deleteCmd.ExecuteNonQuery();
            Console.WriteLine("Data berhasil dihapus!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Kesalahan: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }

    static void SearchData()
    {
        try
        {
            connection.Open();

            Console.Write("Masukkan kata kunci untuk mencari data: ");
            string keyword = Console.ReadLine();

            string searchQuery = @"
                SELECT * FROM data_parkir2
                WHERE jenis_kendaraan LIKE @keyword
                OR merek_kendaraan LIKE @keyword
                OR nama_kendaraan LIKE @keyword
                ORDER BY id";
            MySqlCommand searchCmd = new MySqlCommand(searchQuery, connection);
            searchCmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

            MySqlDataReader reader = searchCmd.ExecuteReader();

            Console.WriteLine("\n=== Hasil Pencarian ===");
            Console.WriteLine("| ID  | Jenis    | Merek     | Nama       | Jumlah | Status    | Waktu Mulai      | Waktu Selesai   | Plat      |");
            Console.WriteLine(new string('-', 95));
            while (reader.Read())
            {
                Console.WriteLine($"| {reader["id"],-4} | {reader["jenis_kendaraan"],-8} | {reader["merek_kendaraan"],-9} | {reader["nama_kendaraan"],-10} | {reader["jumlah"],-6} | {reader["status_kendaraan"],-9} | {reader["waktu_mulai"],-15} | {reader["waktu_selesai"],-15} | {reader["plat"],-8} |");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Kesalahan: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }

    static void FilterData()
    {
        try
        {
            connection.Open();

            Console.Write("Masukkan status untuk memfilter data (contoh: 'Parkir', 'Keluar'): ");
            string filter = Console.ReadLine();

            string filterQuery = "SELECT * FROM data_parkir2 WHERE status_kendaraan = @filter ORDER BY id";
            MySqlCommand filterCmd = new MySqlCommand(filterQuery, connection);
            filterCmd.Parameters.AddWithValue("@filter", filter);

            MySqlDataReader reader = filterCmd.ExecuteReader();

            Console.WriteLine("\n=== Hasil Filter ===");
            Console.WriteLine("| ID  | Jenis    | Merek     | Nama       | Jumlah | Status    | Waktu Mulai      | Waktu Selesai   | Plat      |");
            Console.WriteLine(new string('-', 95));
            while (reader.Read())
            {
                Console.WriteLine($"| {reader["id"],-4} | {reader["jenis_kendaraan"],-8} | {reader["merek_kendaraan"],-9} | {reader["nama_kendaraan"],-10} | {reader["jumlah"],-6} | {reader["status_kendaraan"],-9} | {reader["waktu_mulai"],-15} | {reader["waktu_selesai"],-15} | {reader["plat"],-8} |");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Kesalahan: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }
}
