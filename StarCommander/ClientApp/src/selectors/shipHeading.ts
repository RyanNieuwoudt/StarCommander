import * as R from "ramda";
import { ApplicationState } from "store";

export default (state: ApplicationState) =>
	R.compose(R.defaultTo(0), R.path(["ship", "heading"]))(state) as number;
