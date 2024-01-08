import { Router } from "express";
import * as controller from "../controllers/index";
import * as userController from "../controllers/user";

export const index = Router();

index.get("/", controller.index);
index.get("/user", userController.create);
