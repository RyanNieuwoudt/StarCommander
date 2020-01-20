import React from "react";
import { Box, Heading as HeadingText, RangeInput, Text } from "grommet";

interface HeadingProps {
	onChange: any;
	value: number;
}

export default function Heading(props: HeadingProps) {
	const { onChange, value } = props;

	return (
		<Box direction="column">
			<HeadingText level={3} textAlign="center">
				Heading
			</HeadingText>
			<Box direction="row">
				<RangeInput
					min={0}
					max={359}
					onChange={onChange}
					value={value}
				/>
				<Text>{value.toString().padStart(2, "0")}</Text>
			</Box>
		</Box>
	);
}
