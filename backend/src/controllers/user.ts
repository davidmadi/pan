import { Request, Response } from "express";
import {Sequelize, DataTypes, Model} from 'sequelize';

// Or you can simply use a connection uri
var sequelize = new Sequelize('postgres://postgres:postgres@localhost.com:5432/postgres');

const User = sequelize.define('users', {
  id: {
    type: DataTypes.INTEGER,
    autoIncrement: true,
    primaryKey: true,
    field: 'id',
    allowNull: false
  },
  name: {
    type: DataTypes.STRING,
    field: 'name'
  },
  email: {
    type: DataTypes.STRING,
    field: 'email'
  },
  password: {
    type: DataTypes.STRING,
    field: 'password'
  }
}, {
});



/**
 * GET /
 * Home page.
 */
export const create = async (req: Request, res: Response): Promise<void> => {
    await User.sync({ force: true });
    const jane = User.build({ name: "Jane" });
    await jane.save();
    res.render("create", { title: "Express" });
};

