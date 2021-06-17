import * as R from "ramda";

import { AcknowledgementState } from "store/acknowledgement";

export default R.pathOr<AcknowledgementState>([], ["acknowledgement"]);
