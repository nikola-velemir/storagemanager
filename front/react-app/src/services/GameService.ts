import { error } from "console";
import api from "../infrastructure/Interceptor";
import { Game } from "../model/dummy/Game";

export class GameService {
  async getGames() {
    return api.get<Game[]>("/games");
  }
}
