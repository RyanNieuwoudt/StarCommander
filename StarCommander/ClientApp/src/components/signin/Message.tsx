import * as R from "ramda";
import React from "react";
import { Box, Text } from "grommet";
import { useSelector } from "react-redux";

export default function Message() {
	const message = useSelector(R.path(["auth", "message"])) as string;

	if (!message) {
		return null;
	}

	return (
		<Box>
			<Text>{message}</Text>
		</Box>
	);
}
