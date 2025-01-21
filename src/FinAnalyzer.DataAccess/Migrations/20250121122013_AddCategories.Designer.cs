﻿// <auto-generated />
using System;
using FinAnalyser.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinAnalyser.DataAccess.Migrations
{
    [DbContext(typeof(FinAnalyzerContext))]
    [Migration("20250121122013_AddCategories")]
    partial class AddCategories
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FinAnalyzer.Domain.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.HasKey("Id")
                        .HasName("pk_accounts");

                    b.ToTable("accounts", (string)null);
                });

            modelBuilder.Entity("FinAnalyzer.Domain.Models.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_cathegories");

                    b.ToTable("cathegories", (string)null);
                });

            modelBuilder.Entity("FinAnalyzer.Domain.Models.RawTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateOnly>("Booking")
                        .HasColumnType("date")
                        .HasColumnName("booking");

                    b.Property<string>("BookingText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("booking_text");

                    b.Property<string>("PayerOrPayee")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("payer_or_payee");

                    b.Property<string>("Purpose")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("purpose");

                    b.Property<DateOnly>("ValueDate")
                        .HasColumnType("date")
                        .HasColumnName("value_date");

                    b.HasKey("Id")
                        .HasName("pk_raw_transactions");

                    b.ToTable("raw_transactions", (string)null);
                });

            modelBuilder.Entity("FinAnalyzer.Domain.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("account_id");

                    b.Property<long?>("CathegoryId")
                        .HasColumnType("bigint")
                        .HasColumnName("cathegory_id");

                    b.Property<Guid>("RawTransactionId")
                        .HasColumnType("uuid")
                        .HasColumnName("raw_transaction_id");

                    b.HasKey("Id")
                        .HasName("pk_transactions");

                    b.HasIndex("AccountId")
                        .HasDatabaseName("ix_transactions_account_id");

                    b.HasIndex("CathegoryId")
                        .HasDatabaseName("ix_transactions_cathegory_id");

                    b.HasIndex("RawTransactionId")
                        .HasDatabaseName("ix_transactions_raw_transaction_id");

                    b.ToTable("transactions", (string)null);
                });

            modelBuilder.Entity("FinAnalyzer.Domain.Models.Account", b =>
                {
                    b.OwnsOne("FinAnalyzer.Domain.Models.AccountInfo", "Info", b1 =>
                        {
                            b1.Property<Guid>("AccountId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("AccountHolder")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("info_account_holder");

                            b1.Property<string>("Iban")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("info_iban");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("info_name");

                            b1.HasKey("AccountId");

                            b1.ToTable("accounts");

                            b1.WithOwner()
                                .HasForeignKey("AccountId")
                                .HasConstraintName("fk_accounts_accounts_id");
                        });

                    b.Navigation("Info")
                        .IsRequired();
                });

            modelBuilder.Entity("FinAnalyzer.Domain.Models.RawTransaction", b =>
                {
                    b.OwnsOne("Currency", "Amount", b1 =>
                        {
                            b1.Property<Guid>("RawTransactionId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("amount_amount");

                            b1.Property<int>("Name")
                                .HasColumnType("integer")
                                .HasColumnName("amount_name");

                            b1.HasKey("RawTransactionId");

                            b1.ToTable("raw_transactions");

                            b1.WithOwner()
                                .HasForeignKey("RawTransactionId")
                                .HasConstraintName("fk_raw_transactions_raw_transactions_id");
                        });

                    b.OwnsOne("Currency", "Balance", b1 =>
                        {
                            b1.Property<Guid>("RawTransactionId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("balance_amount");

                            b1.Property<int>("Name")
                                .HasColumnType("integer")
                                .HasColumnName("balance_name");

                            b1.HasKey("RawTransactionId");

                            b1.ToTable("raw_transactions");

                            b1.WithOwner()
                                .HasForeignKey("RawTransactionId")
                                .HasConstraintName("fk_raw_transactions_raw_transactions_id");
                        });

                    b.Navigation("Amount")
                        .IsRequired();

                    b.Navigation("Balance");
                });

            modelBuilder.Entity("FinAnalyzer.Domain.Models.Transaction", b =>
                {
                    b.HasOne("FinAnalyzer.Domain.Models.Account", null)
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("fk_transactions_accounts_account_id");

                    b.HasOne("FinAnalyzer.Domain.Models.Category", "Cathegory")
                        .WithMany()
                        .HasForeignKey("CathegoryId")
                        .HasConstraintName("fk_transactions_categories_cathegory_id");

                    b.HasOne("FinAnalyzer.Domain.Models.RawTransaction", "RawTransaction")
                        .WithMany()
                        .HasForeignKey("RawTransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_transactions_raw_transaction_raw_transaction_id");

                    b.Navigation("Cathegory");

                    b.Navigation("RawTransaction");
                });

            modelBuilder.Entity("FinAnalyzer.Domain.Models.Account", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
