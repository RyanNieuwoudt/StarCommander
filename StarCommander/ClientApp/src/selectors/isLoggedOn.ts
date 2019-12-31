import * as R from "ramda";
import { ApplicationState } from "store";

export default (state: ApplicationState) =>
	R.pathSatisfies(Boolean, ["auth", "token"], state);
