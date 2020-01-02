import React from "react";
import { Box, FormField, Text } from "grommet";

export default function FormFieldLabel(props: any) {
	const { required, label, ...rest } = props;
	return (
		<FormField
			label={
				required ? (
					<Box direction="row">
						<Text>{label}</Text>
						<Text color="status-critical">*</Text>
					</Box>
				) : (
					label
				)
			}
			required={required}
			{...rest}
		/>
	);
}
