import * as R from "ramda";
import { ApplicationState } from "store";
import { Player } from "store/auth";

export default (state: ApplicationState) =>
	R.path<Player>(["auth", "player"], state);
