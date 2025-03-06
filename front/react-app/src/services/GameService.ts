import { error } from "console";
import api from "../infrastructure/Interceptor/Interceptor";
import { Game } from "../model/dummy/Game";

export class GameService {
  static async getGames() {
    return api.get<Game[]>("/games");
  }
}
