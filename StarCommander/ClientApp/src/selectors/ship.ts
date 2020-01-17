import * as R from "ramda";
import { ApplicationState } from "store";
import { ShipState } from "store/ship";

export default (state: ApplicationState) =>
	R.path(["ship"], state) as ShipState;
