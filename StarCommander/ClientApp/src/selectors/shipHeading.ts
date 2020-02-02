import * as R from "ramda";
import { ApplicationState } from "store";

export default (state: ApplicationState) =>
	R.path(["ship", "heading"])(state) as number;
