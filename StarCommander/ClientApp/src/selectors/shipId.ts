import * as R from "ramda";
import { ApplicationState } from "store";

export default (state: ApplicationState) =>
	R.pathOr<string>("", ["ship", "shipId"], state);
