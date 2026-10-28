DROP DATABASE IF EXISTS bank;
CREATE DATABASE bank;
USE bank;

-- Справочник стран
CREATE TABLE country(
    country_id INT NOT NULL AUTO_INCREMENT,
    country_name VARCHAR(100) NOT NULL,
    PRIMARY KEY (country_id),
    UNIQUE (country_name)
);

-- Справочник городов
CREATE TABLE city(
    city_id INT NOT NULL AUTO_INCREMENT,
    country_id INT NOT NULL,
    city_name VARCHAR(100) NOT NULL,
    PRIMARY KEY (city_id),
    UNIQUE (city_name),
    CONSTRAINT city_country_id_foreign FOREIGN KEY(country_id) REFERENCES country(country_id)
);

-- Офисы 
CREATE TABLE office(
    office_id INT NOT NULL AUTO_INCREMENT,
    city_id INT NOT NULL,
    office_name VARCHAR(100) NOT NULL,
    address VARCHAR(255) NOT NULL,
    total_sells_sum DECIMAL(15, 2) DEFAULT 0.00, -- Сумма прибыли офиса
    PRIMARY KEY (office_id),
    CONSTRAINT office_city_id_foreign FOREIGN KEY(city_id) REFERENCES city(city_id)
);

-- Клиенты
CREATE TABLE client(
    client_id INT NOT NULL AUTO_INCREMENT,
    full_name VARCHAR(100) NOT NULL, -- Владелец счета (Имя)
    contact_data VARCHAR(255) NOT NULL, -- Контакты
    PRIMARY KEY (client_id)
);

-- Статусы аккаунтов
CREATE TABLE account_status(
    status_id INT NOT NULL AUTO_INCREMENT,
    status_name VARCHAR(50) NOT NULL,
    PRIMARY KEY (status_id),
    UNIQUE (status_name)
);

-- Счета
CREATE TABLE account(
    account_id INT NOT NULL AUTO_INCREMENT,
    client_id INT NULL, 
    status_id INT NOT NULL,
    office_id INT NOT NULL,
    account_number VARCHAR(20) NOT NULL,
    balance DECIMAL(12, 2) NOT NULL,
    is_system BOOLEAN NOT NULL DEFAULT 0, 
    PRIMARY KEY (account_id),
    UNIQUE (account_number),
    CONSTRAINT account_client_id_foreign FOREIGN KEY(client_id) REFERENCES client(client_id) ON DELETE SET NULL ON UPDATE CASCADE,
    CONSTRAINT account_status_id_foreign FOREIGN KEY(status_id) REFERENCES account_status(status_id),
    CONSTRAINT account_office_id_foreign FOREIGN KEY(office_id) REFERENCES office(office_id)
);

CREATE TABLE `transaction`(
    transaction_id INT NOT NULL AUTO_INCREMENT,
    source_account_id INT NOT NULL,
    destination_account_id INT NOT NULL,
    office_id INT NOT NULL, 
    amount DECIMAL(12, 2) NOT NULL,
    executed_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (transaction_id),
    CONSTRAINT trans_source_fk FOREIGN KEY(source_account_id) REFERENCES account(account_id),
    CONSTRAINT trans_dest_fk FOREIGN KEY(destination_account_id) REFERENCES account(account_id),
    CONSTRAINT trans_office_fk FOREIGN KEY(office_id) REFERENCES office(office_id)
);

-- Страницы сайта 
CREATE TABLE site_page(
    page_id INT NOT NULL AUTO_INCREMENT,
    parent_page_id INT NULL,
    page_name VARCHAR(100) NOT NULL,
    PRIMARY KEY (page_id),
    CONSTRAINT site_page_parent_fk FOREIGN KEY(parent_page_id) REFERENCES site_page(page_id) ON DELETE SET NULL
);