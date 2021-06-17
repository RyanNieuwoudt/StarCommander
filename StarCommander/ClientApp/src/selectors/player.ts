import * as R from "ramda";
import { Player } from "store/auth";

export default R.path<Player>(["auth", "player"]);
