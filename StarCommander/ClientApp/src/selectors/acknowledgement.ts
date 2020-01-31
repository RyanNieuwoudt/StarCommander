import * as R from "ramda";
import { ApplicationState } from "store";
import { AcknowledgementState } from "store/acknowledgement";

export default (state: ApplicationState) =>
	R.path(["acknowledgement"], state) as AcknowledgementState;
