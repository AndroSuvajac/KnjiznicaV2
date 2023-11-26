﻿using System;
using System.Data.SqlClient;

class Program
{
    static string connectionString = "Data Source = DESKTOP - OSKVLPP\\SQLEXPRESS;Database=master; Initial Catalog = Knjiznica2; Integrated Security = True; Pooling=False; TrustServerCertificate=True; Trusted_Connection=True"; // Zamijenite sa svojim vlastitim connection stringom

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("1. Dodaj knjigu");
            Console.WriteLine("2. Dodaj korisnika");
            Console.WriteLine("3. Posudi knjigu");
            Console.WriteLine("4. Vrati knjigu");
            Console.WriteLine("5. Izlaz");

            Console.Write("Odaberite opciju: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DodajKnjigu();
                    break;
                case "2":
                    DodajKorisnika();
                    break;
                case "3":
                    PosudiKnjigu();
                    break;
                case "4":
                    VratiKnjigu();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Nevažeći unos. Pokušajte ponovno.");
                    break;
            }
        }
    }

    static void DodajKnjigu()
    {
        Console.Write("Unesite naziv knjige: ");
        string nazivKnjige = Console.ReadLine();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "INSERT INTO Knjige (Naziv) VALUES (@Naziv)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Naziv", nazivKnjige);
                command.ExecuteNonQuery();
                Console.WriteLine("Knjiga dodana u knjižnicu.");
            }
        }
    }

    static void DodajKorisnika()
    {
        Console.Write("Unesite ime korisnika: ");
        string imeKorisnika = Console.ReadLine();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "INSERT INTO Korisnici (Ime) VALUES (@Ime)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Ime", imeKorisnika);
                command.ExecuteNonQuery();
                Console.WriteLine("Korisnik dodan u knjižnicu.");
            }
        }
    }

    static void PosudiKnjigu()
    {
        Console.Write("Unesite ID korisnika: ");
        int idKorisnika = int.Parse(Console.ReadLine());

        Console.Write("Unesite ID knjige: ");
        int idKnjige = int.Parse(Console.ReadLine());

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "INSERT INTO Posudba (IdKorisnika, IdKnjige, DatumPosudbe) VALUES (@IdKorisnika, @IdKnjige, GETDATE())";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IdKorisnika", idKorisnika);
                command.Parameters.AddWithValue("@IdKnjige", idKnjige);
                command.ExecuteNonQuery();
                Console.WriteLine("Knjiga posuđena korisniku.");
            }
        }
    }

    static void VratiKnjigu()
    {
        Console.Write("Unesite ID posudbe: ");
        int idPosudbe = int.Parse(Console.ReadLine());

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "UPDATE Posudba SET DatumVracanja = GETDATE() WHERE IdPosudbe = @IdPosudbe";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IdPosudbe", idPosudbe);
                command.ExecuteNonQuery();
                Console.WriteLine("Knjiga vraćena u knjižnicu.");
            }
        }
    }
}
