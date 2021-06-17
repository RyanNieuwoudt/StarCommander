import * as R from "ramda";
import { ScannerState } from "store/scanner";
import { Position } from "store/ship";

export default R.pipe(
	R.pathOr<ScannerState>([], ["scanner"]),
	R.map((p: Position) => R.append(-p.y, [p.x]))
);
