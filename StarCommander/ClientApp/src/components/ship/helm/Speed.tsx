import React from "react";
import { Box, Heading, RangeInput, Text } from "grommet";

interface SpeedProps {
	onChange: any;
	value: number;
}

export default function Speed(props: SpeedProps) {
	const { onChange, value } = props;

	return (
		<Box direction="column">
			<Heading level={3} textAlign="center">
				Speed
			</Heading>
			{value !== undefined && (
				<Box direction="row">
					<RangeInput
						min={0}
						max={25}
						onChange={onChange}
						value={value}
					/>
					<Text>{value.toString().padStart(2, "0")}</Text>
				</Box>
			)}
		</Box>
	);
}
