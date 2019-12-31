import React, { useCallback } from "react";
import { useDispatch } from "react-redux";
import { Box, Button, Form, FormField, Heading, Text } from "grommet";
import { actionCreators } from "store/auth";

const FormFieldLabel = props => {
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
};

export default function SignUpPage() {
	const dispatch = useDispatch();

	const onSubmit = useCallback(
		event => {
			const { callSign, firstName, lastName } = event.value;
			dispatch(actionCreators.signUp(callSign, firstName, lastName));
		},
		[dispatch]
	);

	return (
		<Box align="center" pad="large">
			<Heading>Star Commander</Heading>
			<Form onSubmit={onSubmit}>
				<FormFieldLabel name="callSign" label="CallSign" required />
				<FormFieldLabel name="firstName" label="FirstName" required />
				<FormFieldLabel name="LastName" label="LastName" required />
				<Button type="submit" label="Sign up" primary />
				<Text
					margin={{ left: "small" }}
					size="small"
					color="status-critical"
				>
					* Required Field
				</Text>
			</Form>
		</Box>
	);
}
