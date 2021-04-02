import * as R from "ramda";
import { ApplicationState } from "store";

export default (state: ApplicationState) =>
	R.pathOr<number>(0, ["ship", "speed"], state);
