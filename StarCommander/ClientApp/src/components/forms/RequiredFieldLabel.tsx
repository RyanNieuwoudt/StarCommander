import React from "react";
import { Text } from "grommet";

export default function RequiredFieldLabel() {
	return (
		<Text margin={{ left: "small" }} size="small" color="status-critical">
			* Required Field
		</Text>
	);
}
