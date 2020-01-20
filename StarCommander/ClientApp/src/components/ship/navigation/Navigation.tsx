import React from "react";
import { useSelector } from "react-redux";
import { Box, Heading, Text } from "grommet";
import { shipPosition } from "selectors";

export default function Navigation() {
	const position = useSelector(shipPosition);

	if (!position) {
		return null;
	}

	const { x, y } = position;

	return (
		<Box>
			<Heading>X</Heading>
			<Text>{x}</Text>
			<Heading>Y</Heading>
			<Text>{y}</Text>
		</Box>
	);
}
