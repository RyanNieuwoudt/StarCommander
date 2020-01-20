import * as R from "ramda";
import { ApplicationState } from "store";
import { Position } from "store/ship";

export default (state: ApplicationState) =>
	R.path(["ship", "position"], state) as Position | undefined;
