import { Request, Response } from "express";
import { Sequelize } from "sequelize";

/**
 * GET /
 * Home page.
 */
export const index = async (req: Request, res: Response): Promise<void> => {
    var sequelize = new Sequelize('database', 'username', 'password', {
        host: 'localhost',
        dialect: 'postgres',
        pool: {
            max: 5,
            min: 0,
            idle: 10000
        },
        
        // SQLite only
        storage: 'path/to/database.sqlite'
    });
    // Or you can simply use a connection uri
    var sequelize = new Sequelize('postgres://postgres:postgres@localhost.com:5432/postgres');
    res.render("index", { title: "Express" });
};

